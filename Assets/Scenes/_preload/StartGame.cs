using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour {
    private Settings settingsObject;

	// Use this for initialization
	void Start () {
        this.settingsObject = Object.FindObjectOfType<Settings>();
        SceneManager.LoadScene("MainMenu");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayGame()
    {
        Debug.Log("play Game called");
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
