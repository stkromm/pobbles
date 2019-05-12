using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour {
    private AsyncOperation asyncOperation;
    public string sceneName;

    public void LoadByName()
    {
        if(asyncOperation == null || !asyncOperation.isDone)
        {
            asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        }
    }

    public void LoadAdditiveByName()
    {
        if (asyncOperation == null || !asyncOperation.isDone)
        {
            asyncOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        }
    }

    public void LoadSingleByName()
    {
        if (asyncOperation == null || !asyncOperation.isDone)
        {
            asyncOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        }
    }

    public void UnloadScene()
    {
        if (asyncOperation == null || !asyncOperation.isDone)
        {
            asyncOperation = SceneManager.UnloadSceneAsync(sceneName);
        }
        
    }
}
