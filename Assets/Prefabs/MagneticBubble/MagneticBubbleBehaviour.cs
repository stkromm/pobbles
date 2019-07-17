using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticBubbleBehaviour : MonoBehaviour {
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
        lifetime = (int)(maxLifetime * 0.5f);
        gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        lifetime += Time.deltaTime * growthRate;
        moveBubblesTowardsMagnet();
        handleLifetime();
    }

    public void onPop()
    {
        //only pop, if game is not paused
        if (!gameSpeedControllerObject.GetPaused())
        {
            soundObject.PlayMagneticPopSound();
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
        //if bubbles are too old, they pop automatically and points need to be calculated
        if (lifetime > maxLifetime) { 
            soundObject.PlayMagneticPopSound();
            setAllVelocitysToZero();
        }
    }

    void moveBubblesTowardsMagnet()
    {
        BubbleBehaviour[] bubbles = FindObjectsOfType<BubbleBehaviour>();
        NegativeBubbleBehaviour[] negativeBubbles = FindObjectsOfType<NegativeBubbleBehaviour>();
        

        foreach(BubbleBehaviour bubble in bubbles){
            applyForce(bubble);
        }

        foreach (NegativeBubbleBehaviour nbubble in negativeBubbles)
        {
            applyNegativeForce(nbubble);
        }
    }

    void setAllVelocitysToZero()
    {
        BubbleBehaviour[] bubbles = FindObjectsOfType<BubbleBehaviour>();
        NegativeBubbleBehaviour[] negativeBubbles = FindObjectsOfType<NegativeBubbleBehaviour>();


        foreach (BubbleBehaviour bubble in bubbles)
        {
            bubble.GetComponentInParent<Rigidbody2D>().velocity = Vector2.zero;
        }

        foreach (NegativeBubbleBehaviour nbubble in negativeBubbles)
        {
            nbubble.GetComponentInParent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

    void applyForce(BubbleBehaviour bubble)
    {
        float parameter = 1.0f;
        var direction = gameObject.transform.position - bubble.transform.position;
        bubble.GetComponentInParent<Rigidbody2D>().AddForce(parameter * direction.normalized / direction.magnitude);
    }
    void applyNegativeForce(NegativeBubbleBehaviour nbubble)
    {
        float parameter = 1.5f;
        var direction = gameObject.transform.position - nbubble.transform.position;
        nbubble.GetComponentInParent<Rigidbody2D>().AddForce(parameter * direction.normalized / direction.magnitude);
    }
}
