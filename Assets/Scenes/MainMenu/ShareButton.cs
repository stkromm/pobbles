using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
        string playStoreURL = "https://sudden-dev.com/pubbles";
        
        nativeShare.SetText(settingsObject.GetStringFromHashtable("ShareButtonText") + "\n\n " + playStoreURL);
        nativeShare.Share();
        EventSystem.current.SetSelectedGameObject(null);

    }

}
