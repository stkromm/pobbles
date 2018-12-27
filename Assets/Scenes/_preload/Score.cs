using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour {
    private int gamescore = 0;

    public void setGamescore(int score)
    {
        gamescore = score;
    }

    public int getGamescore()
    {
        return gamescore;
    }
}
