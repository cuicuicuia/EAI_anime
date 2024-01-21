using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class main_panel :BasePanel
{
    public void OnPushPanel(string panelName)
    {
        UIPanelType panelType=(UIPanelType) System.Enum.Parse(typeof(UIPanelType), panelName);
        UIManager.Instance.PushPanel(panelType);
    }
    public void OnClosePanel()
    {
        UIManager.Instance.PopPanel();
    }
    private CanvasGroup canvasGroup;
    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public override void OnExit()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
    }
    public override void OnEnter()
    {
        if (canvasGroup == null) { canvasGroup = GetComponent<CanvasGroup>(); }
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }


}


