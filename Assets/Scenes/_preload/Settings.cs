using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour {
    private bool introBool;
    

    private void Awake()
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

}
