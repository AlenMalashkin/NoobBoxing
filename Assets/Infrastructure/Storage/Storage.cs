using System;
using UnityEngine;

public class Storage
{
    public const string money = "Money";
    
    public const string strengthStatProgress = "StrengthStatProgress";
    public const string strengthBoostCount = "StrengthBoostCount";
    public const string strength = "Strength";
    
    public const string guardStatProgress = "GuardStatProgress";
    public const string guardBoostCount = "GuardBoostCount";
    public const string guard = "Guard";

    public const string round = "Round";

    public void Save(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
    }
    
    public void Save(string key, float value)
    {
        PlayerPrefs.SetFloat(key, value);
    }
    
    public void Save(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
    }

    public object Load(string key, StoreDataType type, object valueByDefault)
    {
        switch (type)
        {
            case StoreDataType.Int:
                return PlayerPrefs.GetInt(key, (int)valueByDefault);
            case StoreDataType.Float:
                return PlayerPrefs.GetFloat(key, (float)valueByDefault);
            case StoreDataType.String:
                return PlayerPrefs.GetString(key, (string)valueByDefault);
        }
        
        throw new Exception("Can not load saved data");
    }
}
