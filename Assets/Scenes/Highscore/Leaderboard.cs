using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;
#if UNITY_ANDROID
using GooglePlayGames;
#endif

public class Leaderboard : MonoBehaviour
{

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
    public GameObject socialSignInButtons;
    // ONLINE 
    private IList<string> onlinePlayerList;
    private IList<int> onlineScoreList;
    public Button personalButton;
    public Button globalButton;
    public Button retrySignInButton;
    private Score scoreObject;
    private Settings settingsObject;
    private bool isLocal;

    // Use this for initialization
    void Start()
    {
        // Setup List
        scoreObject = FindObjectOfType<Score>();
        playerList = scoreObject.GetPlayerList();
        scoreList = scoreObject.GetScoreList();

        //show the last game's Score as comparison
        settingsObject = FindObjectOfType<Settings>();
        globalComparisonHeading.gameObject.SetActive(false);
        localComparisonHeading.gameObject.SetActive(true);

        onlinePlayerList = new List<string>();
        onlineScoreList = new List<int>();
        globalButton.onClick.AddListener(delegate
        {
            ClickedGlobal();
        });

        personalButton.onClick.AddListener(delegate
        {
            ClickedPersonal();
        });
        retrySignInButton = socialSignInButtons.GetComponent<Button>();
        retrySignInButton.onClick.AddListener(delegate
        {
            SocialSignin.TrySignIn((success) =>
            {
                if (!isLocal) { 
                    ClickedGlobal();
                }
            });
        });
        personalButton.image.color = Color.grey;
        ClickedPersonal();
        LoadHighscores();
        Social.ShowLeaderboardUI();
    }

    private void SetComparsionEntry(bool local, string name, string score)
    {
        globalComparisonHeading.gameObject.SetActive(!local);
        localComparisonHeading.gameObject.SetActive(local);
        comparisonPlayer.text = name;
        comparisonScore.text = score;
    }

    private void ClickedPersonal()
    {
        Debug.Log("Clicked personal");
        isLocal = true;
        socialSignInButtons.SetActive(false);
        globalButton.image.color = new Color(0xF3 / 256f, 0xA5 / 256f, 0x8F / 256f);
        personalButton.image.color = Color.grey;
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
        
        SetComparsionEntry(
            true,
            scoreObject.GetPlayerPrefsString("lastGamesPlayer"),
            scoreObject.GetPlayerPrefsInt("lastGamesScore").ToString()
        );

    }

    private ILeaderboard GetAllTimeLeaderboard()
    {
        ILeaderboard board = Social.Active.CreateLeaderboard();
        board.id = "classic_alltime";
        board.userScope = UserScope.Global;
        return board;
    }

    private void ClickedGlobal()
    {
        Debug.Log("Clicked global");
        isLocal = false; 
        personalButton.image.color = new Color(0xF3 / 256f, 0xA5 / 256f, 0x8F / 256f);
        globalButton.image.color = Color.grey;
        if (!SocialSignin.IsAuthenticated())
        {
            player0.text = "";
            player1.text = "";
            player2.text = "";
            player3.text = "";
            player4.text = "";
            score0.text = "";
            score1.text = "";
            score2.text = "";
            score3.text = "";
            score4.text = "";
            socialSignInButtons.SetActive(true);
        }
        else if (!Social.Active.GetLoading(GetAllTimeLeaderboard()))
        {

            socialSignInButtons.SetActive(false);
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
        }
        else
        {
            // Show loader
        }
        
        SetComparsionEntry(false, playerList[0], scoreList[0].ToString());
    }

    public void LoadHighscores(){
        resetOnlineList();
        var board = GetAllTimeLeaderboard();

        board.LoadScores(success =>
        {
            if (success){
                Debug.Log("Loading scores for scope: "+board.userScope.ToString());
                var scores = board.scores;
                if (scores.Length > 0)
                {
                    Debug.Log("Loaded scores: " + scores.Length);
                    var maxIndex = Mathf.Min((int)5, (int)scores.Length);
                    var userIds = new string[maxIndex];
                    for (int i = 0; i < maxIndex; i++)
                    {
                        int index = i;
                        IScore score = scores[i];
                        Debug.Log("Score: " + score.value);
                        onlineScoreList.Insert(i, (int)score.value);

                        userIds[i] = score.userID;


                        /*
                        Debug.Log("UserID: " + score.userID);
                        Debug.Log("Load name for ID: " + score.userID);
                        string[] userId = new string[1];
                        userId[0] = score.userID;
                        string username = "Unnamed";
                        Debug.Log("Try to get username");
                        Social.Active.LoadUsers(userId, users =>
                        {
                            Debug.Log("userID: " + userId + " usersCount: " + users.Length);
                            username = users[0].userName;
                            Debug.Log("Username: " + username);
                            onlinePlayerList.Insert(index, username);
                        });*/
                    }
                    // Load usernames;
                    Debug.Log("uidArray: " + userIds);
                    Debug.Log("Loading usernames...");
                    Social.Active.LoadUsers(userIds, users =>
                    {
                        for (int i = 0; i < userIds.Length; i++){
                            IUserProfile user = users[i];
                            Debug.Log("Loaded username: " + user.userName + " for ID: " + user.id);
                            onlinePlayerList.Insert(i, user.userName);
                        }
                    });
                }
            }
            if (!isLocal){
                ClickedGlobal();
            }
        });

        Social.Active.LoadScores("classic_alltime", scores =>
        {

        });

    }

    private void resetOnlineList(){
        onlinePlayerList.Clear();
        onlineScoreList.Clear();
        for (int i = 0; i < 5; i++){
            onlinePlayerList.Add("-");
            onlineScoreList.Add(0);
        }
    }

    private void SetupOnlineLists(Highscoreboard highscoreboard)
    {
        List<LeaderboardEntry> board = highscoreboard.GetBoard();
        for (int i = 0; i < 5; i++)
        {
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
