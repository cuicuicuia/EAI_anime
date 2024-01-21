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
        //联网这里需要更改
        // 检查用户名和密码是否为空
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            messageText.text = "用户名或密码不能为空";
            return;
        }

        // 这里添加您的验证逻辑，例如检查用户名和密码是否匹配
        if (username == "admin" && password == "123123") // 示例的硬编码凭证
        {
            messageText.text = "登录成功";
            IsLogin = true;
            // 这里可以添加登录成功的后续操作，例如场景跳转
        }
        else
        {
            messageText.text = "用户名或密码错误，请重新输入";
            // 可以在这里清空输入字段或者做其他操作
            usernameInputField.text = "";
            passwordInputField.text = "";
        }
    }
}
