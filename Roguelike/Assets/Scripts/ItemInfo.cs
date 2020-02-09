using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfo : MonoBehaviour
{
    public string str1;
    public string str2;
    public string str3;
    public string str4;
    public string str5;

    public string ReturnString()
    {
        string result = $"{str1} \n{str2} \n{str3 } \n{str4} \n{str5}";
        return result;
    }
}
    