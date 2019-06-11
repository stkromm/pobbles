using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof (Settings))]
public class StartGame : MonoBehaviour {
    Settings settingsObject;
    
	void Start () {
        settingsObject = GetComponent<Settings>();
        SceneManager.LoadScene("MainMenu");
	}

    public void PlayGame()
    {
        if (settingsObject.GetIntroBool())
        {
            SceneManager.LoadScene("Introduction");
        }
        else
        {
            SceneManager.LoadScene("GamemodeArcade");
        }
    }
}
