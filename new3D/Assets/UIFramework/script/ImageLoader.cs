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
    public GameObject imagePrefab; // Image Ԥ����
    public Transform imageParent; // ͼƬ�ĸ�������
    private List<string> savedImagePaths = new List<string>(); // �����ͼƬ·���б�
    private string savedImagesKey = "SavedImages"; // PlayerPrefs �����ڱ���ͼƬ·���ļ�
    public Image selectedImage; // ���ڴ洢ѡ�е�ͼƬ
    public Image background; // ������ʾ������UI���

    void Start()
    {
        uploadButton.onClick.AddListener(() => StartCoroutine(ShowLoadDialogCoroutine()));
        LoadSavedImages(); // ����֮ǰ�����ͼƬ
    }

    IEnumerator ShowLoadDialogCoroutine()
    {
        yield return FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.Files, false, null, null, "Load Files", "Load");

        if (FileBrowser.Success)
        {
            string path = FileBrowser.Result[0];
            savedImagePaths.Add(path); // �����·�����б�
            SaveImagePaths(); // ����·���б�
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



    // ����ѡ�е�ͼƬ
    void SetSelectedImage(Image image)
    {
        selectedImage = image;
        // ��������������UI������ִ����������
    }



    // ��ͼƬ�����ʱ�����ô˷���
   public void SetSelectedImage1111()
    {
        if (selectedImage != null && selectedImage.sprite != null)
        {
            // ���±���ͼƬ
            background.sprite = selectedImage.sprite;
            // ��ѡ����������ͼƬ�ĳߴ�����Ӧ�µ�Sprite
            background.SetNativeSize();
        }
    }

}