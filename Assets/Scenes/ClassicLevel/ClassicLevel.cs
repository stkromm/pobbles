using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClassicLevel : MonoBehaviour
{
    public Text usernameText;
    public Text userScoreText;
    public Text levelText;
    private StartGame startGameObject;
    // Holds information about which level is being displayed at the moment
    private int currentLevel = 1;
    // Buttons
    public Button playGameButton;
    public Button nextLevelButton;
    public Button previousLevelButton;
    // Start is called before the first frame update
    void Start()
    {
        if(currentLevel == 1)
        {
            HidePrevLevelButton();
        }
        // Get username and set usernameText
        // Get users current Score/Rank/Level and set userScoreText
        // Get current level, set levelText/currentLevel to current level
        nextLevelButton.onClick.AddListener(ShowNextLevel);
        previousLevelButton.onClick.AddListener(ShowPreviousLevel);
        startGameObject = Object.FindObjectOfType<StartGame>();
        playGameButton.onClick.AddListener(delegate
        {
            startGameObject.PlayGame();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void HidePrevLevelButton()
    {
        previousLevelButton.gameObject.SetActive(false);
    }

    void ShowNextLevel()
    {
        // Add animation to slide over to next level
    }

    void ShowPreviousLevel()
    {
        // Add animation to slide back to previous level
    }

}
