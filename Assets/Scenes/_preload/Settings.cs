﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour {
    private bool introBool;
    private string language;
    private Lang langObject;

    private void Awake()
    {
        LoadIntroBool();
        LoadLanguage();
    }

    //******Introduction************

    private void LoadIntroBool()
    {
        if (PlayerPrefs.HasKey("introBool"))
        {
            // if so, load the stored value
            this.introBool = IntToBool(PlayerPrefs.GetInt("introBool"));
        }
        else
        {
            //else set to default (true)
            this.introBool = true;
        }
    }

    public void SetIntroBool(bool introBool)
    {
        this.introBool = introBool;
        PlayerPrefs.SetInt("introBool", introBool ? 1 : 0);
    }

    public bool GetIntroBool()
    {
        return introBool;
    }

    bool IntToBool(int value)
    {
        if (value == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    //********language support**********
    private void LoadLanguage()
    {
        if (PlayerPrefs.HasKey("language"))
        {
            // if so, load the stored value
            this.language = PlayerPrefs.GetString("language");
        }
        else
        {
            //else set to default system language
            this.language = Application.systemLanguage.ToString();
        }
        //language object initialized with current language as Hashtable
        langObject = new Lang(System.IO.Path.Combine(Application.dataPath, "Lang.xml"), this.language, false);
    }

    public void SetLanguage(string language)
    {
        PlayerPrefs.SetString("language", language);
    }

    public string GetLanguage()
    {
        return this.language;
    }

    public string GetStringFromHashtable(string key)
    {
        return langObject.getString(key);
    }
}