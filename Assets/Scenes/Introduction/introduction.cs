using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class introduction : MonoBehaviour
{
    public Button playGameButton;
    public Toggle introBoolToggle;
    private Settings settingsObject;
    // Start is called before the first frame update
    void Awake()
    {
        this.settingsObject = Object.FindObjectOfType<Settings>();
        //if intro bool false, skip this scene
        if (!settingsObject.GetIntroBool())
        {
            SceneManager.LoadScene("GamemodeArcade");
        }

        playGameButton.onClick.AddListener(delegate
        {
            //set the introBool according to the current toggle state
            settingsObject.SetIntroBool(introBoolToggle.isOn);
            SceneManager.LoadScene("GamemodeArcade");
        });
        
        
    }
    
}
