using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private AsyncOperation loadOperation;

    public void StartLoadingScene(int sceneIndex)
    {
        loadOperation = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Single);
        loadOperation.allowSceneActivation = false;
    }

    public void EnterScene()
    {
        loadOperation.allowSceneActivation = true;
    }
}
