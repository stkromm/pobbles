using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicBoxColliders : MonoBehaviour {
    public BoxCollider2D topBoxCollider;
    public BoxCollider2D leftBoxCollider;
    public BoxCollider2D rightBoxCollider;
    public BoxCollider2D bottomBoxCollider;

    public float topBottomMarginPercentage = 0.1f;
    public float leftRightMargin = 1;

    ScreenOrientation currentOrientation;
	// Use this for initialization
	void Start () {
        currentOrientation = Screen.orientation;
        updateBoxColliders();

    }

    // Update is called once per frame
    void Update () {
        //update colliders if screen orientation is changed
        if (currentOrientation != Screen.orientation)
        {
            Debug.Log("Screen orientation changed. box collider update triggered.");
            updateBoxColliders();
        }
	}

    void updateBoxColliders()
    {
        //top box: whole width and 10% of height for the score and timer
        topBoxCollider.size = new Vector2(Screen.width, Screen.height * topBottomMarginPercentage);
        Debug.Log("Screen width: " + Screen.width + " 10%of screen height: " + Screen.height * topBottomMarginPercentage);
        topBoxCollider.offset = new Vector2(0, -0.5f * (Screen.height - Screen.height * topBottomMarginPercentage));

        //bottomBox: whole width and 10 percent of height for the pause overlay
        bottomBoxCollider.size = new Vector2(Screen.width, Screen.height * topBottomMarginPercentage);
        bottomBoxCollider.offset = new Vector2(0, +0.5f * (Screen.height - Screen.height * topBottomMarginPercentage));

        //leftBox: 1px width and whole height
        leftBoxCollider.size = new Vector2(leftRightMargin, Screen.height);
        leftBoxCollider.offset = new Vector2(-0.5f * (Screen.width - leftRightMargin), 0);

        //rightBox: 1px width and whole height
        rightBoxCollider.size = new Vector2(1, Screen.height);
        rightBoxCollider.offset = new Vector2(+0.5f * (Screen.width - leftRightMargin), 0);
    }
}
