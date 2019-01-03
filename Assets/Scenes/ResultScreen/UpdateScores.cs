using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UpdateScores : MonoBehaviour {
    private int gameScore;
    public Text gameScoreText;

    private int timingScore;
    public Text timingScoreText;

    private int specialsScore;
    public Text specialsScoreText;

    public Text overallScoreText;
    private int overallScore = 0;

    private Score scoreObject;
    public InputField playerNameInput;
    public Button backToMenuButton;
    public Button restartButton;

    // Use this for initialization
    void Start () {
        scoreObject = Object.FindObjectOfType<Score>();
        gameScore = scoreObject.GetGamescore();
        UpdateGameScore(gameScore);

        backToMenuButton.onClick.AddListener(delegate
        {
            scoreObject.RegisterNewScoreInLeaderboard(playerNameInput.text, overallScore);
            SceneManager.LoadScene("MainMenu");
        });
        restartButton.onClick.AddListener(delegate
        {
            scoreObject.RegisterNewScoreInLeaderboard(playerNameInput.text, overallScore);
            SceneManager.LoadScene("GamemodeArcade");
        });

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void UpdateGameScore(int gameScore)
    {
        gameScoreText.text = "" + gameScore;
        UpdateOverallScore(gameScore);
    }

    void UpdateOverallScore(int addScore)
    {
        overallScore = int.Parse(overallScoreText.text) + addScore;
        overallScoreText.text = "" + overallScore;
    }
}
