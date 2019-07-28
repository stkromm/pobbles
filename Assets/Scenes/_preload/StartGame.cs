using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Settings)), RequireComponent(typeof(DDOL))]
public class StartGame : MonoBehaviour
{
    Settings settingsObject;

    void Start()
    {
        Debug.Log("Setup dependencies");
        settingsObject = GetComponent<Settings>();
        Debug.Log("Authentication");
        SocialSignin.TrySignIn(OnAuthResult);
        Debug.Log("Finished game start");
    }

    void OnAuthResult(bool isLoggedIn)
    {
        Debug.Log("Enter OnAuthResult");
        if (isLoggedIn)
        {
            Debug.Log("Logged in username: " + Social.localUser.userName + " with ID: " + Social.localUser.id);
        }
        else
        {
            Debug.Log("Social authentication failed");
        }
        SceneManager.LoadSceneAsync("MainMenu");
        Debug.Log("Leaving OnAuthResult");
    }

    public void PlayGame()
    {
        Debug.Log("play Game called");
        if (settingsObject.GetIntroBool())
        {
            SceneManager.LoadScene("Introduction");
        }
        else
        {
            SceneManager.LoadScene("GamemodeArcade");
        }
    }
}