using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class UIPanelInfo : ISerializationCallbackReceiver
{
    [NonSerialized]
    public UIPanelType panelType;
    public string panelTypeString;
    //{
    //    get
    //    {
    //        return panelType.ToString();
    //    }
    //    set
    //    {
    //        UIPanelType type =(UIPanelType)System.Enum.Parse(typeof(UIPanelType), value);
    //        panelType = type;
    //    }
    //}
    public string path;

    // �����л�   ���ı���Ϣ ������
    public void OnAfterDeserialize()
    {
        UIPanelType type = (UIPanelType)System.Enum.Parse(typeof(UIPanelType), panelTypeString);
        panelType = type;
    }

    public void OnBeforeSerialize()
    {

    }
}
