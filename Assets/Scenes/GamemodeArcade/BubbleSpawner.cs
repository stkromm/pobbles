using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSpawner : MonoBehaviour
{
    public GameObject bubble;
    public GameObject negativeBubble;
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
                float spawnY2 = Random.Range
    (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y + 0.25f, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y - 0.25f - 0.5f);
                float spawnX2 = Random.Range
                    (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x + 0.25f, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x - 0.25f);

                Vector2 spawnPosition2 = new Vector2(spawnX2, spawnY2);
                Instantiate(negativeBubble, spawnPosition2, Quaternion.identity);
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
        //spawn bubbles if not enough bubbles on screen
        if (bubbles.Length < 5)
        {
            Spawn();
        }

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("MouseDown");

            //2D solution
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
            if (hit != null && hit.collider != null)
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

            }
            //3D solution
            // Reset ray with new mouse position
            //RaycastHit[] hits = Physics.RaycastAll(Camera.main.ScreenPointToRay(Input.mousePosition));
            //foreach (RaycastHit hit in hits)
            //{
            //BubbleBehaviour bubble = hit.collider.gameObject.GetComponent(typeof(BubbleBehaviour)) as BubbleBehaviour;
            //if (bubble != null)
            //{
            //    bubble.onPop();
            //}
            //}
        }
    }
}
