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

    public Text newHighScoreText;

    private Score scoreObject;
    public InputField playerNameInput;
    public Button backToMenuButton;
    public Button restartButton;

    public float durationGameScore = 30.0f;
    public float durationTimingScore = 10.0f;
    private float timer = 0.0f;

    // Use this for initialization
    void Start () {
        scoreObject = Object.FindObjectOfType<Score>();
        gameScore = scoreObject.GetGamescore();
        timingScore = scoreObject.GetPerfectTiming() * 100 + scoreObject.GetGoodTiming() * 50;
        specialsScore = 0;

        //overallScore already calculated at the beginning, so if sb skips animation, the right value gets to the leaderboard
        overallScore = gameScore + timingScore + specialsScore;

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
        timer += Time.deltaTime;
        //increase until gameScore is reached
        if (timer < durationGameScore)
        {
            //increase the gameScore
            float progress = timer / durationGameScore;
            int currentScore = (int)Mathf.Lerp(0, gameScore, progress);
            gameScoreText.text = "" + currentScore;
        }
        else if(timer>durationGameScore&&timer<(durationGameScore+durationTimingScore))
        {
            //set the overall score to the gamescore
            overallScoreText.text = "" + gameScore;
            gameScoreText.text = "" + gameScore;

            //increase the timing score
            float progress = (timer-durationGameScore) / durationTimingScore;
            int currentScore = (int)Mathf.Lerp(0, timingScore, progress);
            timingScoreText.text = "" + currentScore;
        }
        //show full score at the End
        else
        {
            overallScoreText.text = "" + (gameScore + timingScore);
            timingScoreText.text = "" + timingScore;
            //check if it is a new highscore
            if(scoreObject.checkForNewHighscore(overallScore) == 0)
            {
                newHighScoreText.text = "New Highscore!";
            }

        }
    }
}
