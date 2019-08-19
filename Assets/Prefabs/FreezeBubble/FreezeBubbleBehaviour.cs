using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeBubbleBehaviour : MonoBehaviour {
    SphereCollider sphereCollider;
    SpriteRenderer spriteRenderer;
    AudioSource audioSource;
    ScoreScript gameScore;
    Sound soundObject;
    GameSpeedController gameSpeedControllerObject;

    float lifetime;
    float growthRate = 20;
    float maxLifetime = 50;

    bool frozen;

	// Use this for initialization
	void Start () {
        soundObject = Object.FindObjectOfType<Sound>();
        sphereCollider = gameObject.GetComponent<SphereCollider>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        audioSource = gameObject.GetComponent<AudioSource>();
        gameSpeedControllerObject = Object.FindObjectOfType<GameSpeedController>();

        ScoreScript[] scores = FindObjectsOfType(typeof(ScoreScript)) as ScoreScript[];
        if(scores.Length == 1)
        {
            gameScore = scores[0];
        }
        lifetime = 0;
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        lifetime += Time.deltaTime * growthRate;
        handleLifetime();
    }

    public void onPop()
    {
        //only pop, if game is not paused
        if (!gameSpeedControllerObject.GetPaused())
        {
            soundObject.PlayFreezePopSound();
            Destroy(gameObject);
        }
        
    }

    void handleLifetime()
    {
        float scale = 1f;
        transform.localScale = new Vector3(scale,scale,scale);
        if (lifetime > maxLifetime)
        {
            Destroy(gameObject);
        }
        else
        {
            //Debug.Log("Lifetime updated to " + lifetime);
        }
    }

    void OnDestroy()
    {
        //if bubbles are too old, they pop automatically
        if (lifetime > maxLifetime) { 
            soundObject.PlayFreezePopSound();
        }
        else
        {
            FreezeTimer();
        }

    }

    void FreezeTimer()
    {
        gameSpeedControllerObject.FreezeTimer();
    }

}
