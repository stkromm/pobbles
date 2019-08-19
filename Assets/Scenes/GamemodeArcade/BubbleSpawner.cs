using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSpawner : MonoBehaviour
{
    public GameObject bubble;
    public GameObject negativeBubble;
    public GameObject magneticBubble;
    public GameObject freezeBubble;
    public float countdown = 3.0f;

    // Use this for initialization
    void Start()
    {
       
    }

    void Spawn()
    {
        
        if (countdown < 0)
        {


            float spawnY = Random.Range
    (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y + 0.25f, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y - 0.25f - 0.5f);
            float spawnX = Random.Range
                (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x + 0.25f, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x - 0.25f);

            Vector2 spawnPosition = new Vector2(spawnX, spawnY);
            Instantiate(bubble, spawnPosition, Quaternion.identity);

            //20% chance for a negativ bubble
            if (Random.Range(0.0f, 1.0f) < 0.2f)
            {

                //random position 2D
                float spawnY2 = Random.Range
    (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y + 0.25f, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y - 0.25f - 0.5f);
                float spawnX2 = Random.Range
                    (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x + 0.25f, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x - 0.25f);
                Vector2 spawnPosition2 = new Vector2(spawnX2, spawnY2);

                //instantiating the bubble
                GameObject nBubble = Instantiate(negativeBubble, spawnPosition2, Quaternion.identity);

                //rotation around z-axis
                nBubble.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0f,360f));
                

            }

            
            if (Random.Range(0.0f, 1.0f) < 0.03f)
            {

                //random position 2D
                float spawnY3 = Random.Range
    (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y + 0.25f, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y - 0.25f - 0.5f);
                float spawnX3 = Random.Range
                    (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x + 0.25f, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x - 0.25f);
                Vector2 spawnPosition3 = new Vector2(spawnX3, spawnY3);

                //instantiating the bubble
                GameObject mBubble = Instantiate(magneticBubble, spawnPosition3, Quaternion.identity);

                //rotation around z-axis
                mBubble.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0f, 360f));


            }

            
                if (Random.Range(0.0f, 1.0f) < 0.03f)
                {

                    //random position 2D
                    float spawnY4 = Random.Range
        (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y + 0.25f, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y - 0.25f - 0.5f);
                    float spawnX4 = Random.Range
                        (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x + 0.25f, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x - 0.25f);
                    Vector2 spawnPosition4 = new Vector2(spawnX4, spawnY4);

                    //instantiating the bubble
                    GameObject fBubble = Instantiate(freezeBubble, spawnPosition4, Quaternion.identity);
                    


                }

        }
    }

    // Update is called once per frame
    void Update()
    {
        //increment countdown
        countdown -= Time.deltaTime;

        
        Object[] bubbles = FindObjectsOfType(typeof(BubbleBehaviour));
        Object[] negativeBubbles = FindObjectsOfType(typeof(NegativeBubbleBehaviour));
        Object[] magneticBubbles = FindObjectsOfType(typeof(MagneticBubbleBehaviour));
        Object[] freezeBubbles = FindObjectsOfType(typeof(FreezeBubbleBehaviour));
        //spawn bubbles if not enough bubbles on screen
        if (bubbles.Length < 5)
        {
            Spawn();
        }
        Touch[] myTouches = Input.touches;
        for (int i = 0; i < Input.touchCount; i++)
        {
            Debug.Log("Input position: "+ i + " " + Input.GetTouch(i).position);
            //2D solution
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

            if (hit.collider != null)
            {
                Debug.Log("Hitting:" + hit.collider.name);
                BubbleBehaviour bubble = hit.collider.gameObject.GetComponent(typeof(BubbleBehaviour)) as BubbleBehaviour;
                if (bubble != null)
                {
                    bubble.onPop();
                }

                NegativeBubbleBehaviour negativeBubble = hit.collider.gameObject.GetComponent(typeof(NegativeBubbleBehaviour)) as NegativeBubbleBehaviour;
                if (negativeBubble != null)
                {
                    negativeBubble.onPop();
                }

                MagneticBubbleBehaviour magneticBubble = hit.collider.gameObject.GetComponent(typeof(MagneticBubbleBehaviour)) as MagneticBubbleBehaviour;
                if (magneticBubble != null)
                {
                    magneticBubble.onPop();
                }

                FreezeBubbleBehaviour freezeBubble = hit.collider.gameObject.GetComponent(typeof(FreezeBubbleBehaviour)) as FreezeBubbleBehaviour;
                if (freezeBubble != null)
                {
                    freezeBubble.onPop();
                }

            }
        }

        //fallback for testing on the PC
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse down");

            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

            if (hit.collider != null)
            {
                Debug.Log("Hitting:" + hit.collider.name);
                BubbleBehaviour bubble = hit.collider.gameObject.GetComponent(typeof(BubbleBehaviour)) as BubbleBehaviour;
                if (bubble != null)
                {
                    bubble.onPop();
                }

                NegativeBubbleBehaviour negativeBubble = hit.collider.gameObject.GetComponent(typeof(NegativeBubbleBehaviour)) as NegativeBubbleBehaviour;
                if (negativeBubble != null)
                {
                    negativeBubble.onPop();
                }

                MagneticBubbleBehaviour magneticBubble = hit.collider.gameObject.GetComponent(typeof(MagneticBubbleBehaviour)) as MagneticBubbleBehaviour;
                if (magneticBubble != null)
                {
                    magneticBubble.onPop();
                }

                FreezeBubbleBehaviour freezeBubble = hit.collider.gameObject.GetComponent(typeof(FreezeBubbleBehaviour)) as FreezeBubbleBehaviour;
                if (freezeBubble != null)
                {
                    freezeBubble.onPop();
                }
            }
        }
    }
}
