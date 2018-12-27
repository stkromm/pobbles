using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    public float timeLeft = 60.0f;
    int score;
    public Text finalScoreText;
    public Text scoreText;
    public Text timerText;
    public Canvas resultMenu;

    // Use this for initialization
    void Start()
    {
        scoreText.text = "" + score;
        timerText.text = "1:00";
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (Mathf.Round(timeLeft) >= 60)
        {
            int minutes = Mathf.FloorToInt(timeLeft / 60.0f);
            timerText.text = "" + minutes + ":" + Mathf.Floor(timeLeft-minutes*60).ToString("00");
        }
        else
        {
            if(timeLeft < 1)
            {
                timerText.text = "0:00";
            }
            else
            {
                timerText.text = "" + Mathf.Floor(timeLeft).ToString("0:00");
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
            Score scoreObject = Object.FindObjectOfType<Score>();
            scoreObject.setGamescore(score);
            //Load ResultScreen
            SceneManager.LoadScene("ResultScreen");
        }
    }

    public void processBubblePop(float lifetime, float maxLifetime)
    {
        if (timeLeft < 0)
        {
            return;
        }
        if (lifetime >= maxLifetime)
        {
            score -= 100;
            Debug.Log("New score:" + score);
        }
        else
        {
            score += 150 - (int)(100f * (lifetime / maxLifetime));
            Debug.Log("New score:" + score);
        }
        scoreText.text = "" + score;
       
    }
}
