using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

}
