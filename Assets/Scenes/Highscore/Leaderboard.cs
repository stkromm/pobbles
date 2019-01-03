using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour {
    private IList<string> playerList;
    public Text player0;
    public Text player1;
    public Text player2;
    public Text player3;
    public Text player4;

    private IList<int> scoreList;
    public Text score0;
    public Text score1;
    public Text score2;
    public Text score3;
    public Text score4;

    private Score scoreObject;
    // Use this for initialization
    void Start () {
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

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
