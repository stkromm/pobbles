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
    public Button saveScoreButton;
    public Button restartButton;
    bool scoreSaved = false;

    public float durationGameScore = 30.0f;
    public float durationTimingScore = 10.0f;
    private float timer = 0.0f;

    private StartGame startGameObject;
    private Settings settingsObject;

    // Use this for initialization
    void Start () {
        settingsObject = Object.FindObjectOfType<Settings>();
        scoreObject = Object.FindObjectOfType<Score>();
        startGameObject = Object.FindObjectOfType<StartGame>();
        gameScore = scoreObject.GetGamescore();
        timingScore = scoreObject.GetPerfectTiming() * 100 + scoreObject.GetGoodTiming() * 50;
        specialsScore = 0;

        //overallScore already calculated at the beginning, so if sb skips animation, the right value gets to the leaderboard
        overallScore = gameScore + timingScore + specialsScore;

        playerNameInput.text = SocialSignin.IsAuthenticated() ? Social.Active.localUser.userName : "Player";
        backToMenuButton.onClick.AddListener(delegate
        {
            //prevent multile score saving
            
            SaveScore(playerNameInput.text, overallScore);
            SceneManager.LoadScene("MainMenu");
        });
        restartButton.onClick.AddListener(delegate
        {
            SaveScore(playerNameInput.text, overallScore);
            startGameObject.PlayGame();
        });
        saveScoreButton.onClick.AddListener(delegate
        {
            SaveScore(playerNameInput.text, overallScore);
        });


    }

	private void SaveScore(string name, int score)
    {
        if (SocialSignin.IsAuthenticated()) {
            SaveScoreIfAuthenticated(name, score);
        }
        else
        {
            SocialSignin.TrySignIn(success =>
            {
                if (success)
                {
                    SaveScoreIfAuthenticated(name, score);
                }
            });
        }

        //prevent multiple saving and disable button
        if (!scoreSaved)
        {
            scoreObject.RegisterNewScoreInLeaderboard(playerNameInput.text, overallScore);
            scoreSaved = true;
            saveScoreButton.interactable = false;
            saveScoreButton.image.color = Color.grey;

            HighscoreHandler handler = new HighscoreHandler();
            handler.WriteLeaderboard(playerNameInput.text, overallScore);
        }
        
    }

    private void SaveScoreIfAuthenticated(string name, int score)
    {
        Social.Active.ReportScore(score, "classic_alltime", (bool success) =>
        {
            if (success)
            {
                Debug.Log("Successfully uploaded score");
            }
            else
            {
                Debug.Log("Failed to upload score");
            }
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
                newHighScoreText.gameObject.SetActive(true);
            }
        }
    }
}
