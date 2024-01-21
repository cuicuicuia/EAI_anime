using UnityEngine;
using System.Collections;

public class GameRoot : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
       // Debug.Log(UIPanelType.login_panel);
        UIManager.Instance.PushPanel(UIPanelType.login_panel);
    }

}
