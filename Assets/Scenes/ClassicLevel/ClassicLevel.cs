using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClassicLevel : MonoBehaviour
{
    private StartGame startGameObject;

    // Holds information about which level is being displayed at the moment
    private int currentLevel = 1;
    // Buttons
    public Button playGameButton;

    // Start is called before the first frame update
    void Start()
    {

        startGameObject = Object.FindObjectOfType<StartGame>();
        playGameButton.onClick.AddListener(delegate
        {
            Debug.Log("PlayGame Pressed");
            startGameObject.PlayGame();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

}
