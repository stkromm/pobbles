using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleBehaviour : MonoBehaviour {
    SphereCollider sphereCollider;
    SpriteRenderer spriteRenderer;
    AudioSource audioSource;
    ScoreScript gameScore;
    Sound soundObject;
    GameSpeedController gameSpeedControllerObject;

    float lifetime;
    float growthRate = 20;
    float maxLifetime = 200;

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
        lifetime = (int)(Random.value * (maxLifetime * 0.5f));
        spriteRenderer.color = Color.white;
    }
	
	// Update is called once per frame
	void Update ()
    {
        lifetime += Time.deltaTime * growthRate;
        spriteRenderer.color = Color.Lerp(Color.white, Color.red, lifetime / maxLifetime);
        handleLifetime();
    }

    public void onPop()
    {
        //pop sound not played from audioSource of Gameobject itself, because gameobject is destroyed before sound is played
        //only pop, if game is not paused
        if (!gameSpeedControllerObject.GetPaused())
        {
            soundObject.PlayPopSound();
            gameScore.processBubblePop(lifetime, maxLifetime, gameObject.transform.position);
            Destroy(gameObject);
        }
        
    }

    void handleLifetime()
    {
        float scale = (lifetime / maxLifetime) * 1.25f + 0.2f;
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
        
    }
}
