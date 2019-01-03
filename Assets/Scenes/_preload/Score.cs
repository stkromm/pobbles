using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour {
    private int gamescore = 0;
    private string scoreDefaultKey = "score";
    private string playerDefaultKey = "player";
    IList<int> scoreList;
    IList<string> playerList;

    private void Start()
    {
        BuildScoreList();
        BuildPlayerList();
    }
    //keep track of the gamescore
    public void SetGamescore(int score)
    {
        gamescore = score;
    }

    public int GetGamescore()
    {
        return gamescore;
    }

    public void RegisterNewScoreInLeaderboard(string player, int score)
    {
        int index = -1;
        //check if new score is inside the top 5
        for(int i = scoreList.Count-1; i>-1; i--)
        {
            if(score > scoreList[i])
            {
                //set the index to the highest score that is lower than the new score
                index = i;
            }
        }

        //if the score is inside, register the new score with the corresponding player
        if (index != -1)
        {
            //if no player name is given
            if(player == "")
            {
                player = "Random User";
            }
            //insert the score at the correct position
            scoreList.Insert(index, score);
            playerList.Insert(index, player);

            //remove the lowest score
            scoreList.RemoveAt(scoreList.Count-1);
            playerList.RemoveAt(playerList.Count-1);

            //write the new PlayerPrefs
            for (int i = 0; i < scoreList.Count; i++)
            {
                Debug.Log("writing Loop i: " + i);
                PlayerPrefs.SetInt(scoreDefaultKey + i, scoreList[i]);
                PlayerPrefs.SetString(playerDefaultKey + i, playerList[i]);
            }
        }
    }

    private void BuildScoreList()
    {
        scoreList = new List<int>();
        //key score1 holds the best score ... key score5 holds the last score in leaderboard
        for(int i = 0; i < 5; i++){
            scoreList.Add(GetPlayerPrefsInt(scoreDefaultKey + i));
        }
    }
    public IList<int> GetScoreList()
    {
        return scoreList;
    }

    private void BuildPlayerList()
    {
        playerList = new List<string>();
        //key player 1 holds the best score ... key player5 holds the last score in leaderboard
        for (int i = 0; i < 5; i++)
        {
            playerList.Add(GetPlayerPrefsString(playerDefaultKey + i));
        }
    }

    public IList<string> GetPlayerList()
    {
        return playerList;
    }

    string GetPlayerPrefsString(string key)
    {
        
        if (PlayerPrefs.HasKey(key))
        {
            return PlayerPrefs.GetString(key);
        }
        else
        {
            return "-";
        }
    }

    int GetPlayerPrefsInt(string key)
    {

        if (PlayerPrefs.HasKey(key))
        {
            return PlayerPrefs.GetInt(key);
        }
        else
        {
            return 0;
        }
    }
}
