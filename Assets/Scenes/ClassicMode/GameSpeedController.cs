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
    public Image frozenTimerImage;
    public Canvas pauseCanvas;
    private bool paused=false;
    private bool frozen = false;

    private StartGame startGameObject;
    private int numberOfFreezeBubblesHitInParallel = 0;
    private void Awake()
    {
        startGameObject = Object.FindObjectOfType<StartGame>();
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
            startGameObject.PlayGame();
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
        
        //show the freeze bubble next to the timer, when it is active
        frozenTimerImage.gameObject.SetActive(frozen);
        
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
        if (paused & frozen)
        {
            paused = false;
            Time.timeScale = 0;
            pauseCanvas.gameObject.SetActive(paused);
        }else if(paused)
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

    public void FreezeTimer()
    {
        StartCoroutine(Freeze(2.0f));
        //activate Frozen Image over timer, implement Frozen Bubbles in Bubble Spwaner
    }

    private IEnumerator Freeze(float seconds)
    {
        //set the frozen bool and the timeScale to 0
        frozen = true;
        Time.timeScale = 0.0f;

        //set a flag to track the first iteration of the while loop during a pause press
        bool pauseFirstIterationFlag = true;

        //increment the number of freeze bubbles, that are hit in paralle to prevent an execution of time scale 1
        numberOfFreezeBubblesHitInParallel++;

        //set the freeze end time based on the real time sinc estart up, so timescale is not used
        float freezeEndTime = Time.realtimeSinceStartup + seconds;
        float deltaFETandRTSS = 0.0f;

        //pause the execution for 2 secs
        while (Time.realtimeSinceStartup < freezeEndTime)
        {
            //catch when the game is paused and freeze was hitted before
            if (pauseFirstIterationFlag & paused)
            {
                //only set the delta the first time the pause is detected
                deltaFETandRTSS = freezeEndTime - Time.realtimeSinceStartup;

                //set the flag to true
                pauseFirstIterationFlag = false;
            }

            //as long as the game is paused, also pause the freeze state
            if (paused)
            {
                freezeEndTime = Time.realtimeSinceStartup + deltaFETandRTSS;
            }
            Debug.Log("Freeze Loop");
            yield return 0;
            
        }
        numberOfFreezeBubblesHitInParallel--;
        //set the frozen bool and timeScale to 1, when there is no other frozen bubble hit. Otherwise, let the other coroutine reset the values, when the other freeze end time is reached
        if (numberOfFreezeBubblesHitInParallel == 0)
        {
            frozen = false;
            Time.timeScale = 1;
        }
    }

    public bool GetFrozenState()
    {
        return this.frozen;
    }
}
