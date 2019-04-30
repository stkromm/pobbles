using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Button playGameButton;
    //private StartGame startGameObject;
    // Start is called before the first frame update
    void Start()
    {
        //startGameObject = Object.FindObjectOfType<StartGame>();
        playGameButton.onClick.AddListener(delegate
        {
            SceneManager.LoadScene("SelectLevel");
            //startGameObject.PlayGame();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
