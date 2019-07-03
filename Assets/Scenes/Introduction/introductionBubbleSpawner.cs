using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class introductionBubbleSpawner : MonoBehaviour
{
    public Image placeholderPositiveBubble;
    public Image placeholderNegativeBubble;

    public GameObject positiveBubble;
    public GameObject negativeBubble;

    GameObject spawnedPositiveBubble;
    GameObject spawnedNegativeBubble;
    bool waitPositive;
    bool waitNegative;
    // Start is called before the first frame update
    void Start()
    {
        //Set the wait flags and start coroutines
        waitPositive = true;
        StartCoroutine(IntroSpawnPositive(0));
        waitNegative = true;
        StartCoroutine(IntroSpawnNegative(0));
    }

    IEnumerator IntroSpawnPositive(int wait)
    {
        yield return new WaitForSeconds(wait);
        Vector2 spawnPos = new Vector2(placeholderPositiveBubble.transform.position.x, placeholderPositiveBubble.transform.position.y);
        spawnedPositiveBubble = Instantiate(positiveBubble, spawnPos, Quaternion.identity);
        waitPositive = false;
    }

    IEnumerator IntroSpawnNegative(int wait)
    {
        yield return new WaitForSeconds(wait);
        Vector2 spawnPos = new Vector2(placeholderNegativeBubble.transform.position.x, placeholderNegativeBubble.transform.position.y);
        spawnedNegativeBubble = Instantiate(negativeBubble, spawnPos, Quaternion.identity);
        //rotation around z-axis
        spawnedNegativeBubble.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0f, 360f));
        waitNegative = false;

    }

    // Update is called once per frame
    void Update()
    {
        //check for objects and w8 flags
        if(spawnedNegativeBubble == null & !waitNegative)
        {
            waitNegative = true;
            StartCoroutine(IntroSpawnNegative(1));
        }
        if (spawnedPositiveBubble == null & !waitPositive)
        {
            waitPositive = true;
            StartCoroutine(IntroSpawnPositive(1));
        }

        Touch[] myTouches = Input.touches;
        for (int i = 0; i < Input.touchCount; i++)
        {

            //2D solution
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

            if (hit.collider != null)
            {
                Debug.Log("Hitting:" + hit.collider.name);
                IntroPositiveBubbleBehaviour bubble = hit.collider.gameObject.GetComponent(typeof(IntroPositiveBubbleBehaviour)) as IntroPositiveBubbleBehaviour;
                if (bubble != null)
                {
                    bubble.onPop();
                    
                }

                IntroNegativeBubbleBehaviour negativeBubble = hit.collider.gameObject.GetComponent(typeof(IntroNegativeBubbleBehaviour)) as IntroNegativeBubbleBehaviour;
                if (negativeBubble != null)
                {
                    negativeBubble.onPop();
                    
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
                IntroPositiveBubbleBehaviour bubble = hit.collider.gameObject.GetComponent(typeof(IntroPositiveBubbleBehaviour)) as IntroPositiveBubbleBehaviour;
                if (bubble != null)
                {
                    bubble.onPop();
                }

                IntroNegativeBubbleBehaviour negativeBubble = hit.collider.gameObject.GetComponent(typeof(IntroNegativeBubbleBehaviour)) as IntroNegativeBubbleBehaviour;
                if (negativeBubble != null)
                {
                    negativeBubble.onPop();
                }

            }
        }
    }
}
