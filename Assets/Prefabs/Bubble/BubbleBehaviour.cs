using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleBehaviour : MonoBehaviour {
    SphereCollider sphereCollider;
    SpriteRenderer spriteRenderer;
    ScoreScript gameScore;

    float lifetime;
    float growthRate = 20;
    float maxLifetime = 200;

	// Use this for initialization
	void Start () {
        sphereCollider = gameObject.GetComponent<SphereCollider>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
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
        Debug.Log("Bubble popped after " + lifetime);
        Destroy(gameObject);
    }

    void handleLifetime()
    {
        float scale = (lifetime / maxLifetime) * 1.25f + 0.1f;
        transform.localScale = new Vector3(scale,scale,scale);
        if (lifetime > maxLifetime)
        {
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Lifetime updated to " + lifetime);
        }
    }

    void OnDestroy()
    {
        gameScore.processBubblePop(lifetime, maxLifetime);
    }
}
