using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShareButton : MonoBehaviour
{
    private Settings settingsObject;
    private void Start()
    {
        settingsObject = Object.FindObjectOfType<Settings>();
    }
    public void ShareApp()
    {
        //TODO setup native share as in readme file
        NativeShare nativeShare = new NativeShare();
        //share url stores
        string playStoreURL = "https://pobbles-dev.firebaseapp.com/";
        string appStoreURL = "https://pobbles-dev.firebaseapp.com/";
        //TODO include translation later
        nativeShare.SetText(settingsObject.GetStringFromHashtable("ShareButtonText") + "\n\n Android: " + playStoreURL + "\n\n iOS: " + appStoreURL);
        nativeShare.Share();
    }

}
