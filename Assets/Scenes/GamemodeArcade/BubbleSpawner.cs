using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSpawner : MonoBehaviour
{
    public GameObject bubble;

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < 5; ++i)
        {
            Spawn();
        }
    }

    void Spawn()
    {

        float spawnY = Random.Range
(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y+0.25f, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y-0.25f-0.5f);
        float spawnX = Random.Range
            (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x+0.25f, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x-0.25f);

        Vector2 spawnPosition = new Vector2(spawnX, spawnY);
        Instantiate(bubble, spawnPosition, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        Object[] bubbles = FindObjectsOfType(typeof(BubbleBehaviour));
        if (bubbles.Length < 5)
        {
            Spawn();
        }

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("MouseDown");
            // Reset ray with new mouse position
            RaycastHit[] hits = Physics.RaycastAll(Camera.main.ScreenPointToRay(Input.mousePosition));
            foreach (RaycastHit hit in hits)
            {
                BubbleBehaviour bubble = hit.collider.gameObject.GetComponent(typeof(BubbleBehaviour)) as BubbleBehaviour;
                if (bubble != null)
                {
                    bubble.onPop();
                }
            }
        }
    }
}
