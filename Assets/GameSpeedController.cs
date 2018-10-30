using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSpeedController : MonoBehaviour {
    public Canvas canvas;
    bool paused;

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
            canvas.enabled = paused;
        }
    }

    public void Resume()
    {
        if (paused)
        {
            paused = false;
            Time.timeScale = 1;
            canvas.enabled = paused;
        }
    }
}
