using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// ��Dictory����չ
/// </summary>
public static class DictionaryExtension
{

    /// <summary>
    /// ���Ը���key�õ�value���õ��˵Ļ�ֱ�ӷ���value��û�еõ�ֱ�ӷ���null
    /// this Dictionary<Tkey,Tvalue> dict ����ֵ��ʾ����Ҫ��ȡֵ���ֵ�
    /// </summary>
    public static Tvalue TryGet<Tkey, Tvalue>(this Dictionary<Tkey, Tvalue> dict, Tkey key)
    {
        Tvalue value;
        dict.TryGetValue(key, out value);
        return value;
    }

}
