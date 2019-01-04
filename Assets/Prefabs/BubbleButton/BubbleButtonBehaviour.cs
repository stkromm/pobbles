using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BubbleButtonMovement : MonoBehaviour {
    public float maxScaleSize;
    public int pulseSpeed;
    public int delay;
    Vector3 initScale;
    private Sound soundObject;
    // Use this for initialization
    void Start () {
        initScale = this.gameObject.transform.localScale;

        //play pop sound if button is pressed
        gameObject.GetComponent<Button>().onClick.AddListener(delegate
        {
            soundObject.PlayPopSound();
        });
	}
	
	// Update is called once per frame
	void Update () {

        //transform the Scale
            this.gameObject.transform.localScale = initScale + new Vector3(maxScaleSize, maxScaleSize) * (pulseSpeed * Mathf.Sin(Time.realtimeSinceStartup+delay));
        
	}

    

}
