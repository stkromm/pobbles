using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateScores : MonoBehaviour {
    private int gameScore;
    public Text gameScoreText;
    private int timingScore;
    public Text timingScoreText;
    private int specialsScore;
    public Text specialsScoreText;
    public Text overallScoreText;

    // Use this for initialization
    void Start () {
        Score scoreObject = Object.FindObjectOfType<Score>();
        gameScore = scoreObject.getGamescore();
        UpdateGameScore(gameScore);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void UpdateGameScore(int gameScore)
    {
        gameScoreText.text = "" + gameScore;
        updateOverallScore(gameScore);
    }

    void updateOverallScore(int addScore)
    {
        int overallScore = int.Parse(overallScoreText.text) + addScore;
        overallScoreText.text = "" + overallScore;
    }
}
