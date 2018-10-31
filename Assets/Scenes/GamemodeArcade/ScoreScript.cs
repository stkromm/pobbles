using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    public int maxBubbles = 30;
    int score;
    int bubbles;
    public Text finalScoreText;
    public Text scoreText;
    public Text bubbleText;
    public Canvas resultMenu;

    // Use this for initialization
    void Start()
    {
        scoreText.text = "" + score;
        bubbleText.text = "Bubbles:" + (30 - bubbles);
    }

    public void processBubblePop(float lifetime, float maxLifetime)
    {
        if (bubbles >= maxBubbles)
        {
            return;
        }
        if (lifetime >= maxLifetime)
        {
            score -= 50;
            Debug.Log("New score:" + score);
        }
        else
        {
            score += 50 + (int)(100f * (lifetime / maxLifetime));
            Debug.Log("New score:" + score);
        }
        bubbles++;
        scoreText.text = "" + score;
        bubbleText.text = "Bubbles:" + (30 - bubbles);
        if (bubbles >= maxBubbles)
        {
            Component[] bubbles = FindObjectsOfType(typeof(BubbleBehaviour)) as Component[];
            foreach(Component c in bubbles)
            {
                Destroy(c.gameObject);
            }
            Destroy(FindObjectOfType(typeof(BubbleSpawner)) as Component);
            scoreText.enabled = false;
            bubbleText.enabled = false;
            resultMenu.enabled = true;
            
            PlayerStats stats = gameObject.GetComponentInParent(typeof(PlayerStats)) as PlayerStats;

            var oldScore = stats.getHighScore();
            stats.saveScore(score);

            finalScoreText.text = (oldScore != stats.getHighScore() ? "New Highscore" : "Score ") + score;

        }
    }
}
