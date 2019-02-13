using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class introduction : MonoBehaviour
{
    public Button playGameButton;
    public Toggle introBoolToggle;
    private Settings settingsObject;

    public Button forwardButton;
    public Button backwardButton;
    private int introPageCount = 1;
    private int maxPageCount = 3;
    public Text pageText;
    public Canvas intro1;
    public Canvas intro2;
    public Canvas intro3;


    // Start is called before the first frame update
    void Awake()
    {
        this.settingsObject = Object.FindObjectOfType<Settings>();
        //if intro bool false, skip this scene
        if (!settingsObject.GetIntroBool())
        {
            SceneManager.LoadScene("GamemodeArcade");
        }
        

        playGameButton.onClick.AddListener(delegate
        {
            //set the introBool according to the current toggle state
            settingsObject.SetIntroBool(introBoolToggle.isOn);
            SceneManager.LoadScene("GamemodeArcade");
        });

        //handle forward button
        forwardButton.onClick.AddListener(delegate
        {
            introPageCount += 1;
            //if forward at the end
            if (introPageCount > maxPageCount)
            {
                introPageCount = 1;
            }
        });

        //handle backward button
        backwardButton.onClick.AddListener(delegate
        {
            introPageCount -= 1;
            if (introPageCount < 1)
            {
                introPageCount = maxPageCount;
            }
        });
    }

    private void Update()
    {
        UpdatePage();
    }

    void UpdatePage()
    {
        //disable enable intro pages
        DisableAllIntroCanvas();

        //enable the current intro page
        EnableIntroCanvas();

        //update the page text
        UpdatePageCountText();
    }

    void UpdatePageCountText()
    {
        pageText.text = introPageCount + " " + settingsObject.GetStringFromHashtable(pageText.name) + " " + maxPageCount;
    }

    void DisableAllIntroCanvas()
    {
        intro1.gameObject.SetActive(false);
        intro2.gameObject.SetActive(false);
        intro3.gameObject.SetActive(false);
    }

    void EnableIntroCanvas()
    {
        if (introPageCount == 1)
        {
            intro1.gameObject.SetActive(true);
        }else if (introPageCount == 2)
        {
            intro2.gameObject.SetActive(true);
        }
        else if (introPageCount == 3)
        {
            intro3.gameObject.SetActive(true);
        }
    }
}
