using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroScoreScript : MonoBehaviour
{
    public Canvas uiCanvas;
    private Settings settingsObject;

    public GameObject bubbleScoreObject;

    // Use this for initialization
    void Start()
    {
        settingsObject = Object.FindObjectOfType<Settings>();
        
    }

    void Update()
    {
    
    }

    public void ProcessPositiveBubblePop(float lifetime, float maxLifetime, Vector3 position)
    {
        int popScore = CalculateBubbleScore(lifetime, maxLifetime);

        //get timing for the bubble pop
        string timing = CalculateTiming(lifetime, maxLifetime);

        //make the pop score floating over a bubble after pop
        GameObject bubbleScoreObjectClone = Instantiate(bubbleScoreObject, transform.position, transform.rotation, uiCanvas.transform);
        bubbleScoreObjectClone.GetComponent<BubbleScoreBehaviour>().UpdateComponents(popScore, position, timing);
    }

    public void ProcessNegativeBubblePop(float lifetime, float maxLifetime, Vector3 position)
    {
        int popScore = CalculateNegativeBubbleScore(lifetime, maxLifetime);

        //get timing for the bubble pop
        string timing = CalculateNegativeTiming(lifetime, maxLifetime);

        //make the pop score floating over a bubble after pop
        GameObject bubbleScoreObjectClone = Instantiate(bubbleScoreObject, transform.position, transform.rotation, uiCanvas.transform);
        bubbleScoreObjectClone.GetComponent<BubbleScoreBehaviour>().UpdateComponents(popScore, position, timing);

    }


    private int CalculateBubbleScore(float lifetime, float maxLifetime)
    {
        int score;
        if (lifetime >= maxLifetime)
        {
            score = -100;
        }
        else
        {
            score = 150 - (int)(100f * (lifetime / maxLifetime));
        }
        return score;
    }

    private string CalculateTiming(float lifetime, float maxLifetime)
    {
        if (lifetime >= maxLifetime)
        {
            return settingsObject.GetStringFromHashtable("RedBubbleDied");
        }
        else
        {
            //keep track of timing
            //perfect = 10% of max lifetime
            if (lifetime < 0.1f * maxLifetime)
            {
                //vibrate the phone
                Handheld.Vibrate();
                return settingsObject.GetStringFromHashtable("PerfectTiming");
            }//good = 20% of max lifetime
            else if (lifetime < 0.2f * maxLifetime)
            {
                return settingsObject.GetStringFromHashtable("GoodTiming");
            }
            else
            {
                return "";
            }
        }
    }
    private int CalculateNegativeBubbleScore(float lifetime, float maxLifetime)
    {
        int score;
        if (lifetime >= maxLifetime)
        {
            score = +100;
        }
        else
        {
            score = -50 - (int)(100f * (lifetime / maxLifetime));
        }
        return score;
    }

    private string CalculateNegativeTiming(float lifetime, float maxLifetime)
    {
        if (lifetime >= maxLifetime)
        {
            return settingsObject.GetStringFromHashtable("NegativBubbleDied");
        }
        else
        {
            return settingsObject.GetStringFromHashtable("NegativeBubbleClicked");

        }
    }

}
