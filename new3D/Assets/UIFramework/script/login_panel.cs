using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class login_panel : BasePanel
{
    // Start is called before the first frame update
    private CanvasGroup canvasGroup;

    public InputField usernameInputField;
    public InputField passwordInputField;
    public Text messageText;
   // public Boolean count = true;
    public Boolean IsLogin = false;
    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public override void OnExit()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
    }
    public void OnClosePanel()
    {
        OnLoginButtonClicked();
        if (IsLogin == false) { return ; }
        UIManager.Instance.PopPanel();
    }
    public override void OnEnter()
    {
        if (canvasGroup == null) { canvasGroup = GetComponent<CanvasGroup>(); }
     
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }
    public void OnPushPanel(string panelName)
    {
       
        OnLoginButtonClicked();
        if (IsLogin==false ){return; }
        UIPanelType panelType1 = (UIPanelType)System.Enum.Parse(typeof(UIPanelType), panelName);
        UIManager.Instance.PushPanel(panelType1);
    }
    public void OnLoginButtonClicked()
    {
        string username = usernameInputField.text;
        string password = passwordInputField.text;
        //����������Ҫ����
        // ����û����������Ƿ�Ϊ��
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            messageText.text = "�û��������벻��Ϊ��";
            return;
        }

        // �������������֤�߼����������û����������Ƿ�ƥ��
        if (username == "admin" && password == "123123") // ʾ����Ӳ����ƾ֤
        {
            messageText.text = "��¼�ɹ�";
            IsLogin = true;
            // ���������ӵ�¼�ɹ��ĺ������������糡����ת
        }
        else
        {
            messageText.text = "�û����������������������";
            // ������������������ֶλ�������������
            usernameInputField.text = "";
            passwordInputField.text = "";
        }
    }
}
