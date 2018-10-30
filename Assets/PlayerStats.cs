using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {
    public int highestScore;

    public int getHighScore()
    {
        return highestScore;
    }

    public void saveScore(int score)
    {
        if(score > highestScore)
        {
            highestScore = score;
        }
    }

    void Start()
    {
        if (PlayerPrefs.HasKey("highScore"))
        {
            highestScore = PlayerPrefs.GetInt("highScore");
        }
    }

    void OnDestroy()
    {
        PlayerPrefs.SetInt("highScore", highestScore);
    }
}
