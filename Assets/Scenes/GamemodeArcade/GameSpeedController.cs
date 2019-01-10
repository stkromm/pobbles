using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSpeedController : MonoBehaviour {
    public Button resumeButton;
    public Button stopButton;
    public Button exitButton;
    public Button restartButton;

    public Canvas pauseCanvas;
    private bool paused=false;

    private void Awake()
    {
        resumeButton.onClick.AddListener(delegate
        {
            Resume();
        });

        stopButton.onClick.AddListener(delegate
        {
            Stop();
        });
        
        exitButton.onClick.AddListener(delegate
        {
            SceneManager.LoadScene("ResultScreen");
        });

        restartButton.onClick.AddListener(delegate
        {
            SceneManager.LoadScene("GamemodeArcade");
        });
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
            if(paused)
            {
                Resume();
            }
            else
            {
                Stop();
            }
        }
    }

    public void Stop()
    {
        if (!paused)
        {
            paused = true;
            Time.timeScale = 0;
            pauseCanvas.gameObject.SetActive(paused);
        }
    }

    public void Resume()
    {
        Debug.Log("Resume called");
        if (paused)
        {
            paused = false;
            Time.timeScale = 1;
            pauseCanvas.gameObject.SetActive(paused);
        }
    }
    public bool GetPaused()
    {
        return paused;
    }

    private void OnDestroy()
    {
        Time.timeScale = 1;
    }
}
