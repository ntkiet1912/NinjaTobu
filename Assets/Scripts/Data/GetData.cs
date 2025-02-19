using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetData : MonoBehaviour
{
    public int GetIntData(string keyname , int value)
    {
        return PlayerPrefs.GetInt(keyname, value);
    }
    public string GetStringData(string keyname, string value)
    {
        return PlayerPrefs.GetString(keyname, value);
    }
    public float GetFloatData(string keyname, float value)
    {
        return PlayerPrefs.GetFloat(keyname, value);
    }
}
