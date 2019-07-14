﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroPositiveBubbleBehaviour : MonoBehaviour
{
    SphereCollider sphereCollider;
    SpriteRenderer spriteRenderer;
    Sound soundObject;
    IntroScoreScript introScoreScript;
    private Settings settingsObject;

    float lifetime;
    float growthRate = 20;
    float maxLifetime = 200;

    // Use this for initialization
    void Start()
    {
        soundObject = Object.FindObjectOfType<Sound>();
        sphereCollider = gameObject.GetComponent<SphereCollider>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        settingsObject = Object.FindObjectOfType<Settings>();
        introScoreScript = Object.FindObjectOfType<IntroScoreScript>();

        lifetime = (int)(Random.value * (maxLifetime * 0.5f));
        spriteRenderer.color = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        lifetime += Time.deltaTime * growthRate;
        spriteRenderer.color = Color.Lerp(Color.white, Color.red, lifetime / maxLifetime);
        handleLifetime();
    }

    public void onPop()
    {
        
        soundObject.PlayPopSound();
        introScoreScript.ProcessPositiveBubblePop(lifetime, maxLifetime, gameObject.transform.position);
        Destroy(gameObject);
        

    }

    void handleLifetime()
    {
        float scale = (lifetime / maxLifetime) * 1.25f + 0.2f;
        transform.localScale = new Vector3(scale, scale, scale);
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
        if (lifetime > maxLifetime)
        {
            soundObject.PlayPopSound();
            introScoreScript.ProcessPositiveBubblePop(lifetime, maxLifetime, gameObject.transform.position);
        }
    }
    
}