﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BubbleScoreBehaviour : MonoBehaviour {
    public float lifetime = 5.0f;
	
    public void UpdateComponents(int score, Vector3 position, string timing)
    {
        gameObject.transform.position = new Vector2(position.x,position.y);
        if (score < 0)
        {
            //bubble popped itself
            gameObject.GetComponent<Text>().color = Color.red;
        }
        else
        {
            gameObject.GetComponent<Text>().color = Color.green;
        }
        if (timing == "")
        {
            //if empty, do not concatenate timing so the score is in the middle of the bubble
            gameObject.GetComponent<Text>().text = "" + score;
        }
        else
        {
            gameObject.GetComponent<Text>().text = timing + "\n" + "\n" + score;
        }
        
        
        
        
    }

    void Start () {
        Debug.Log("Bubble Score object created.");
    }

	// Update is called once per frame
	void Update () {

        //destroy bubble score text when lifetime over
        lifetime -= Time.deltaTime;
        if (lifetime < 0)
        {
            Destroy(gameObject);
        }
	}
}
