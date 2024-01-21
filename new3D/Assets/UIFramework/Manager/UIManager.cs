using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using DG.Tweening;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine.UI;

public class UIManager
{
    /// 
    /// ����ģʽ�ĺ���
    /// 1������һ����̬�Ķ��� �������� ���ڲ�����
    /// 2�����췽��˽�л�

    private static UIManager _instance;

    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new UIManager();
            }
           // Debug.Log("testassess");
            return _instance;
        }
    }

    private Transform canvasTransform;
    private Transform CanvasTransform
    {
        get
        {
            if (canvasTransform == null)
            {
                canvasTransform = GameObject.Find("main_Canvas").transform;
            }
            
            return canvasTransform;
        }
    }
    private Dictionary<UIPanelType, string> panelPathDict;//�洢�������Prefab��·��
    private Dictionary<UIPanelType, BasePanel> panelDict;//��������ʵ����������Ϸ�������ϵ�BasePanel���
    private Stack<BasePanel> panelStack;

    private UIManager()
    {
        ParseUIPanelTypeJson();
    }

    /// <summary>
    /// ��ĳ��ҳ����ջ��  ��ĳ��ҳ����ʾ�ڽ�����
    /// </summary>
    public void PushPanel(UIPanelType panelType)
    {
      
        if (panelStack == null)
            panelStack = new Stack<BasePanel>();

        if (panelStack.Count > 0)
        {
            BasePanel topPanel = panelStack.Peek();
            CanvasGroup topCanvas = topPanel.GetComponent<CanvasGroup>();
            topCanvas.DOFade(0, 0f).OnComplete(() => topPanel.OnPause());
        }

        BasePanel panel = GetPanel(panelType);
        CanvasGroup canvasGroup = panel.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0; // ��ʼ��͸����Ϊ0
        canvasGroup.DOFade(1, 0.5f).OnComplete(() => panel.OnEnter());
        panelStack.Push(panel);
    } 
    /// <summary>
    /// ��ջ ����ҳ��ӽ������Ƴ�
    /// </summary>
    public void PopPanel()
    {
       
        if (panelStack == null)
            panelStack = new Stack<BasePanel>();

        if (panelStack.Count <= 0) return;

        BasePanel topPanel = panelStack.Pop();
        CanvasGroup topCanvas = topPanel.GetComponent<CanvasGroup>();
        topCanvas.DOFade(0, 0.5f).OnComplete(() => {
            topPanel.OnExit();
            if (panelStack.Count > 0)
            {
                BasePanel nextPanel = panelStack.Peek();
                CanvasGroup nextCanvas = nextPanel.GetComponent<CanvasGroup>();
                nextCanvas.DOFade(1, 0f).OnComplete(() => nextPanel.OnResume());
            }
        });

    }

    /// <summary>
    /// ����������� �õ�ʵ���������
    /// </summary>
    /// <returns></returns>
    private BasePanel GetPanel(UIPanelType panelType)
    {
        if (panelDict == null)
        {
            panelDict = new Dictionary<UIPanelType, BasePanel>();
        }

        //BasePanel panel;
        //panelDict.TryGetValue(panelType, out panel);//TODO

        BasePanel panel = panelDict.TryGet(panelType);
        if (panel == null)
        {
            //����Ҳ�������ô�����������prefab��·����Ȼ��ȥ����prefabȥʵ�������
            //string path;
            //panelPathDict.TryGetValue(panelType, out path);
            string path = panelPathDict.TryGet(panelType);
            GameObject instPanel = GameObject.Instantiate(Resources.Load(path)) as GameObject;
            instPanel.transform.SetParent(CanvasTransform, false);
            panelDict.Add(panelType, instPanel.GetComponent<BasePanel>());
            return instPanel.GetComponent<BasePanel>();
        }
        else
        {
            return panel;
        }

    }

    [Serializable]
    class UIPanelTypeJson
    {
        public List<UIPanelInfo> infoList;

    }
    private void ParseUIPanelTypeJson()
    {
        panelPathDict = new Dictionary<UIPanelType, string>();

        TextAsset ta = Resources.Load<TextAsset>("UIPanelType");

        UIPanelTypeJson jsonObject = JsonUtility.FromJson<UIPanelTypeJson>(ta.text);

        foreach (UIPanelInfo info in jsonObject.infoList)
        {
            //Debug.Log(info.panelType);
            panelPathDict.Add(info.panelType, info.path);
        }
    }

    /// <summary>
    /// just for test
    /// </summary>
   
}
