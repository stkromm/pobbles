using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BubbleButtonBehaviour : MonoBehaviour {
    public float maxScaleSize;
    public int pulseSpeed;
    public int delay;
    Vector3 initScale;
    private Sound soundObject;
    // Use this for initialization
    void Start () {
        Debug.Log("Start function called.");
        initScale = this.gameObject.transform.localScale;

        //play pop sound if button is pressed
        soundObject = soundObject = Object.FindObjectOfType<Sound>();
        gameObject.GetComponent<Button>().onClick.AddListener(delegate
        {
           
            Debug.Log("Listener triggered");
            soundObject.PlayPopSound();
        });
	}
	
	// Update is called once per frame
	void Update () {

        //transform the Scale
            this.gameObject.transform.localScale = initScale + new Vector3(maxScaleSize, maxScaleSize) * (pulseSpeed * Mathf.Sin(Time.realtimeSinceStartup+delay));
        
	}

    

}
