using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelChanger : MonoBehaviour
{
    public Animator animator;

    public string NextLevel;
    public string PreviousLevel;
    public Button NextLevelButton;
    public Button PreviousLevelButton;

    private void Start()
    {
        NextLevelButton.onClick.AddListener(delegate
        {
            FadeToNextLevel();
        });

        PreviousLevelButton.onClick.AddListener(delegate
        {
            FadeToPreviousLevel();
        });

        if(PreviousLevel == "")
        {
            PreviousLevelButton.gameObject.SetActive(false);
        }

        if (NextLevel == "")
        {
            NextLevelButton.gameObject.SetActive(false);
        }
    }
    private string levelToLoad;

    public void FadeToNextLevel()
    {
        levelToLoad = NextLevel;
        animator.SetTrigger("FadeOut");
    }

    public void FadeToPreviousLevel()
    {
        levelToLoad = PreviousLevel;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
