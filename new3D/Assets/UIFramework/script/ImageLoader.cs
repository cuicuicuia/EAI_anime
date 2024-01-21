using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using SimpleFileBrowser;
using UnityEngine.Networking;
using System.IO;
using System.Collections.Generic;
using System;

public class ImageLoader : MonoBehaviour
{
    public Button uploadButton;
    public GameObject imagePrefab; // Image 预制体
    public Transform imageParent; // 图片的父级容器
    private List<string> savedImagePaths = new List<string>(); // 保存的图片路径列表
    private string savedImagesKey = "SavedImages"; // PlayerPrefs 中用于保存图片路径的键
    public Image selectedImage; // 用于存储选中的图片
    public Image background; // 用于显示背景的UI组件

    void Start()
    {
        uploadButton.onClick.AddListener(() => StartCoroutine(ShowLoadDialogCoroutine()));
        LoadSavedImages(); // 加载之前保存的图片
    }

    IEnumerator ShowLoadDialogCoroutine()
    {
        yield return FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.Files, false, null, null, "Load Files", "Load");

        if (FileBrowser.Success)
        {
            string path = FileBrowser.Result[0];
            savedImagePaths.Add(path); // 添加新路径到列表
            SaveImagePaths(); // 保存路径列表
            StartCoroutine(LoadImage(path));
        }
        else
        {
            Debug.Log("No file was chosen");
            yield break;
        }
    }

    IEnumerator LoadImage(string filePath)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture("file:///" + filePath);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Texture2D texture = DownloadHandlerTexture.GetContent(request);
            CreateImageInstance(texture);
        }
        else
        {
            Debug.LogError("Image load failed: " + request.error);
        }
    }

    void CreateImageInstance(Texture2D texture)
    {
        GameObject newImageObj = Instantiate(imagePrefab, imageParent);
        Image imageComponent = newImageObj.GetComponent<Image>();
        imageComponent.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f,
0.5f));
        newImageObj.GetComponent<Button>().onClick.AddListener(() => SetSelectedImage(imageComponent));
        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)imageParent);
    }

private void SaveImagePaths()
    {
        string joinedPaths = string.Join(";", savedImagePaths.ToArray());
        PlayerPrefs.SetString(savedImagesKey, joinedPaths);
        PlayerPrefs.Save();
    }

    private void LoadSavedImages()
    {
        string savedData = PlayerPrefs.GetString(savedImagesKey, "");
        if (!string.IsNullOrEmpty(savedData))
        {
            string[] paths = savedData.Split(';');
            foreach (var path in paths)
            {
                if (File.Exists(path))
                {
                    StartCoroutine(LoadImage(path));
                }
            }
        }
    }



    // 设置选中的图片
    void SetSelectedImage(Image image)
    {
        selectedImage = image;
        // 你可以在这里更新UI背景或执行其他操作
    }



    // 当图片被点击时，调用此方法
   public void SetSelectedImage1111()
    {
        if (selectedImage != null && selectedImage.sprite != null)
        {
            // 更新背景图片
            background.sprite = selectedImage.sprite;
            // 可选：调整背景图片的尺寸以适应新的Sprite
            background.SetNativeSize();
        }
    }

}