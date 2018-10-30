using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour {
    public int sceneIndex;

    public void LoadByIndex()
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
