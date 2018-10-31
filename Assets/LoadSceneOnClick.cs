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
}
