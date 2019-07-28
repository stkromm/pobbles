using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;
using System.Threading.Tasks;

public class HighscoreHandler
{
    public void WriteLeaderboard(string username, int score)
    {
    }
}

// CUSTOM CLASSES

// LeaderboardEntry
// STRUCTURE: uid:[name:score]
[Serializable]
public class LeaderboardEntry : System.IComparable<LeaderboardEntry>
{
    private string uid;
    private string username;
    private int score;

    // For received Values
    public LeaderboardEntry(string uid, string name, int score)
    {
        this.uid = uid;
        this.username = name;
        this.score = score;
    }
    // For new Values
    public LeaderboardEntry(string name, int score)
    {
        // Later use Firebase UID
        System.DateTime dt = System.DateTime.Now;
        this.uid = dt.Day.ToString() + dt.Month.ToString() + dt.Second.ToString() + name;
        this.username = name;
        this.score = score;
    }

    public string GetName()
    {
        return username;
    }

    public int GetScore()
    {
        return score;
    }

    public string GetUID()
    {
        return uid;
    }

    public int CompareTo(LeaderboardEntry entry)
    {
        // A null value means that this object is greater.
        if (entry == null)
            return 1;

        else
            return this.score.CompareTo(entry.score);
    }
}

// Highscoreboard
// STRUCTURE: List<LeaderboardEntry> => List<uid:[name:score]>
public class Highscoreboard
{
    private List<LeaderboardEntry> board;
    private int listSize = 5;

    //list.RemoveRange(index, list.Count - index);

    public Highscoreboard()
    {
        board = new List<LeaderboardEntry>();
    }

    // Sorts board Descending
    public void SortBoard()
    {
        board.Sort((a, b) => b.CompareTo(a));
    }
    // Cuts list if more than maxSize has been added
    public void DropLowestScores()
    {
        if (board.Count > listSize)
        {
            board.RemoveRange(listSize, board.Count - listSize);
        }
    }
    // Drops only single lowest score
    public string GetLowestScoreUIDToDelete()
    {
        SortBoard();
        if (board.Count <= listSize)
        {
            return null;
        }
        LeaderboardEntry lastEntry = board[board.Count - 1];
        return lastEntry.GetUID();
    }
    // Inserts new object if its in highscore list
    public bool InsertEntry(LeaderboardEntry entry)
    {
        bool inserted = false;
        if (board.Count == 0)
        {
            board.Add(entry);
            inserted = true;
        }
        // If entry needs to be added last in a not full list
        else if (board.Count < listSize && board.Last().GetScore() > entry.GetScore())
        {
            board.Insert(board.Count, entry);
            inserted = true;
        }
        else
        {
            int minimum = Mathf.Min(board.Count, listSize);
            for (int i = 0; i < minimum; i++)
            {
                LeaderboardEntry entryAtIndex = board.ElementAt(i);
                if (entryAtIndex.GetScore() < entry.GetScore())
                {
                    board.Insert(i, entry);
                    return true;
                }
            }
        }
        return inserted;
    }

    public List<LeaderboardEntry> GetBoard()
    {
        return board;
    }

}
