﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    public float timeLeft = 60.0f;
    public float countdown = 3.0f;
    int score;
    public Text finalScoreText;
    public Text gameStartText;
    public Text scoreText;
    public Text timerText;
    public Text gameEndText;
    public Canvas uiCanvas;
    public Canvas resultMenu;
    private Sound soundObject;
    private Score scoreObject;
    private int perfectTiming=0;
    private int goodTiming=0;
    private int normalTiming = 0;
    private int poppedItself = 0;

    public GameObject bubbleScoreObject;

    // Use this for initialization
    void Start()
    {
        scoreText.text = "0";
        timerText.text = "1:00";
        gameStartText.text = "Pop the bubbles! \n \n" + (countdown).ToString("0");

        //start the arcade game music
        soundObject = Object.FindObjectOfType<Sound>();
        soundObject.PlayArcadeGameMusic();

        //reference score object
        scoreObject = Object.FindObjectOfType<Score>();
        }

    void Update()
    {
        //countdown before the game starts
        countdown -= Time.deltaTime;
        gameStartText.text = "Pop the bubbles! \n \n" + (countdown).ToString("0");
        //bubble spawner needs to w8 for countdown aswell
        if (countdown < 0)
        {
            gameStartText.text = "";

            timeLeft -= Time.deltaTime;
            if (Mathf.Round(timeLeft) >= 60)
            {
                int minutes = Mathf.FloorToInt(timeLeft / 60.0f);
                timerText.text = "" + minutes + ":" + Mathf.Floor(timeLeft - minutes * 60).ToString("00");
            }
            else
            {
                if (timeLeft < 1)
                {
                    timerText.text = "0:00";
                }
                else
                {
                    timerText.text = "" + Mathf.Floor(timeLeft).ToString("0:00");
                }

                //color timer text before game end
                if (timeLeft < 4)
                {
                    timerText.color = Color.red;
                }

            }

            if (timeLeft < 0)
            {
                Component[] bubbles = FindObjectsOfType(typeof(BubbleBehaviour)) as Component[];
                foreach (Component c in bubbles)
                {
                    Destroy(c.gameObject);
                }
                Destroy(FindObjectOfType(typeof(BubbleSpawner)) as Component);

                /*
                scoreText.enabled = false;
                bubbleText.enabled = false;
                resultMenu.enabled = true;

                PlayerStats stats = gameObject.GetComponentInParent(typeof(PlayerStats)) as PlayerStats;

                var oldScore = stats.getHighScore();
                stats.saveScore(score);

                finalScoreText.text = (oldScore != stats.getHighScore() ? "New Highscore" : "Score ") + score;
                */

                //set gamescore in gamemanager (preload scene)
                scoreObject.SetGamescore(score);

                //announce the game end
                gameEndText.text = "Good Job!";

            }
            if (timeLeft < -2)
            {
                //switch back to the menu music
                soundObject.PlayMenuGameMusic();
                //Load ResultScreen
                SceneManager.LoadScene("ResultScreen");
            }
        }
    }

    public void processBubblePop(float lifetime, float maxLifetime, Vector3 position)
    {
        // do nothing is bubble is popped after game end (used for the last bubbles that are on the screen upon game end)
        if (timeLeft < 0)
        {
            return;
        }

        //adjust the score text
        int popScore = CalculateBubbleScore(lifetime, maxLifetime);
        
        score += popScore;
        scoreText.text = "" + score;

        //get timing for the bubble pop
        string timing = CalculateTiming(lifetime, maxLifetime);

        //make the pop score floating over a bubble after pop
        GameObject bubbleScoreObjectClone = Instantiate(bubbleScoreObject, transform.position, transform.rotation,uiCanvas.transform);
        bubbleScoreObjectClone.GetComponent<BubbleScoreBehaviour>().UpdateComponents(popScore, position, timing);
    }

    
    private int CalculateBubbleScore(float lifetime, float maxLifetime)
    {
        int score;
        if (lifetime >= maxLifetime)
        {
            score = -100;
            Debug.Log("New score:" + score);
        }
        else
        {
            score = 150 - (int)(100f * (lifetime / maxLifetime));
            Debug.Log("New score:" + score);
        }
        return score;
    }

    private string CalculateTiming(float lifetime, float maxLifetime)
    {
        if (lifetime >= maxLifetime)
        {
            poppedItself += 1;
            return "Upsi!";
        }
        else
        {
            //keep track of timing
            //perfect = 10% of max lifetime
            if (lifetime < 0.1f * maxLifetime)
            {
                perfectTiming += 1;
                return "Perfect!";
            }//good = 20% of max lifetime
            else if (lifetime < 0.2f * maxLifetime)
            {
                goodTiming += 1;
                return "Good!";
            }
            else
            {
                normalTiming += 1;
                return "";
            }
        }
    }

    private void OnDestroy()
    {
        //set the number of bubbles with their corresponding timing
        scoreObject.setTiming(normalTiming, goodTiming, perfectTiming, poppedItself);
    }
}
