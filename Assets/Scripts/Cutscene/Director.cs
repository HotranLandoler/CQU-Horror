using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Director : MonoBehaviour
{
    public void GameInit()
    {
        GameManager.Instance.AddStartItems();
    }

    public void OnCgStarted()
    {
        GameManager.Instance.OnCgStarted();
    }

    public void OnCgFinished()
    {
        GameManager.Instance.OnCgFinished();
    }

    public void ChangeSanity(int val)
    {
        GameManager.Instance.Sanity += val;
    }

    public void GotoScene(int sceneID)
    {
        if (sceneID == 0) Destroy(GameManager.Instance.gameObject);
        SceneManager.LoadScene(0);
    }

}
