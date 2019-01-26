using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NegativeBubbleBehaviour : MonoBehaviour {
    SphereCollider sphereCollider;
    SpriteRenderer spriteRenderer;
    AudioSource audioSource;
    ScoreScript gameScore;
    Sound soundObject;
    GameSpeedController gameSpeedControllerObject;

    float lifetime;
    float growthRate = 20;
    float maxLifetime = 100;

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
        spriteRenderer.color = Color.black;
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
            soundObject.PlayPopSound();
            gameScore.ProcessNegativeBubblePop(lifetime, maxLifetime, gameObject.transform.position);
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
        //if bubbles are too old, they pop automatically and points need to be calculated
        if (lifetime > maxLifetime) { 
            soundObject.PlayPopSound();
            gameScore.ProcessNegativeBubblePop(lifetime, maxLifetime, gameObject.transform.position);
        }
    }
}
