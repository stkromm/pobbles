 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;

public class Leaderboard : MonoBehaviour {

    private IList<string> playerList;
    public Text player0;
    public Text player1;
    public Text player2;
    public Text player3;
    public Text player4;
    public Text comparisonPlayer;

    private IList<int> scoreList;
    public Text score0;
    public Text score1;
    public Text score2;
    public Text score3;
    public Text score4;
    public Text comparisonScore;

    public Text localComparisonHeading;
    public Text globalComparisonHeading;

    // ONLINE 
    private IList<string> onlinePlayerList;
    private IList<int> onlineScoreList;
    public Button personalButton;
    public Button globalButton;
    private bool updateToGlobal = false;
    private bool updateToLocal = false;
    private bool isLocal = true;

    private Score scoreObject;
    private Settings settingsObject;

    // Use this for initialization
    void Start () {
        // Setup List
        scoreObject = GameObject.FindObjectOfType<Score>();
        playerList = scoreObject.GetPlayerList();
        scoreList = scoreObject.GetScoreList();
        Debug.Log("player 0: " + playerList[0].ToString());
        player0.text = playerList[0];
        Debug.Log("player 0: " + player0.text);
        player1.text = playerList[1];
        player2.text = playerList[2];
        player3.text = playerList[3];
        player4.text = playerList[4];
        score0.text = scoreList[0].ToString();
        score1.text = scoreList[1].ToString();
        score2.text = scoreList[2].ToString();
        score3.text = scoreList[3].ToString();
        score4.text = scoreList[4].ToString();

        //show the last game's Score as comparison
        settingsObject = GameObject.FindObjectOfType<Settings>();
        globalComparisonHeading.gameObject.SetActive(false);
        localComparisonHeading.gameObject.SetActive(true);
        comparisonPlayer.text = scoreObject.GetPlayerPrefsString("lastGamesPlayer");
        comparisonScore.text = "" + scoreObject.GetPlayerPrefsInt("lastGamesScore");

        onlinePlayerList = new List<string>();
        onlineScoreList = new List<int>();
        Social.localUser.Authenticate((bool success) =>
        {
            if (success)
            {
                LoadHighscores();
            }
        });

        globalButton.onClick.AddListener(delegate
        {
            ClickedGlobal();
        });

        personalButton.onClick.AddListener(delegate
        {
            ClickedPersonal();
        });

        personalButton.image.color = Color.grey;

        //Social.ShowLeaderboardUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (updateToGlobal)
        {
            player0.text = onlinePlayerList[0];
            player1.text = onlinePlayerList[1];
            player2.text = onlinePlayerList[2];
            player3.text = onlinePlayerList[3];
            player4.text = onlinePlayerList[4];
            score0.text = onlineScoreList[0].ToString();
            score1.text = onlineScoreList[1].ToString();
            score2.text = onlineScoreList[2].ToString();
            score3.text = onlineScoreList[3].ToString();
            score4.text = onlineScoreList[4].ToString();
            updateToGlobal = false;

            //show best offline Score as comparison
            localComparisonHeading.gameObject.SetActive(false);
            globalComparisonHeading.gameObject.SetActive(true);
            comparisonPlayer.text = playerList[0];
            comparisonScore.text = scoreList[0].ToString();
        }
        else if (updateToLocal)
        {
            player0.text = playerList[0];
            player1.text = playerList[1];
            player2.text = playerList[2];
            player3.text = playerList[3];
            player4.text = playerList[4];
            score0.text = scoreList[0].ToString();
            score1.text = scoreList[1].ToString();
            score2.text = scoreList[2].ToString();
            score3.text = scoreList[3].ToString();
            score4.text = scoreList[4].ToString();
            updateToLocal = false;

            //show the last game's Score as comparison
            globalComparisonHeading.gameObject.SetActive(false);
            localComparisonHeading.gameObject.SetActive(true);
            comparisonPlayer.text = scoreObject.GetPlayerPrefsString("lastGamesPlayer");
            comparisonScore.text = "" + scoreObject.GetPlayerPrefsInt("lastGamesScore");
        }
    }
    private void ClickedPersonal(){
        // Switch to personal highscore list
        Debug.Log("Clicked Personal");

        //get the orange on the global button and then color the personal button in grey
        globalButton.image.color = personalButton.image.color;
        personalButton.image.color = Color.grey;
        updateToLocal = true;
    }

    private void ClickedGlobal(){
        // Switch to global highscore list
        Debug.Log("clicked global");
        personalButton.image.color = globalButton.image.color;
        globalButton.image.color = Color.grey;
        updateToGlobal = true;
    }

    public void LoadHighscores()
    {
        // GameCenter Leaderboard
        ILeaderboard board = Social.CreateLeaderboard();
        board.id = "classic_alltime";
        board.LoadScores(success =>
        {
            if (success){
                foreach (IScore score in board.scores){
                    Debug.Log("Loaded Score: " + score.formattedValue);
                    Highscoreboard hb = new Highscoreboard();
                    LeaderboardEntry e = new LeaderboardEntry(null, score.userID, (int)score.value);
                    hb.GetBoard().Add(e);
                    Debug.Log("Added to board: " + e.GetName() + " with score: " + e.GetScore());
                    SetupOnlineLists(hb);
                }
            }
        });
    }

    private void SetupOnlineLists(Highscoreboard highscoreboard){
        List<LeaderboardEntry> board = highscoreboard.GetBoard();
        onlinePlayerList.Clear();
        onlineScoreList.Clear();
        for (int i = 0; i < 5; i++) {
            if (board.Count > i)
            {
                onlinePlayerList.Insert(i, board[i].GetName());
                onlineScoreList.Insert(i, board[i].GetScore());
            }
            else
            {
                onlinePlayerList.Add("-");
                onlineScoreList.Add(0);
            }

        }

    }
}
