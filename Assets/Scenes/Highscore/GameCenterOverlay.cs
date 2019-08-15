using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCenterOverlay : MonoBehaviour
{

    public Button gameCenterSetUpButton;
    public Button gameCenterBackButton;
    public Button socialSigninButton;
    public GameObject gameCenterOverlay;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Script on disabled object called.");
        socialSigninButton.onClick.AddListener(delegate
        {
            gameCenterOverlay.SetActive(true);
            Debug.Log("Button pressed");
        });

        gameCenterSetUpButton.onClick.AddListener(delegate
        {
            gameCenterOverlay.SetActive(false);
            LinkToIOSSettings();
        });
        gameCenterBackButton.onClick.AddListener(delegate
        {
            gameCenterOverlay.SetActive(false);
        });
    }

    void LinkToIOSSettings()
    {
        //open iOS game center settings
    }
}
