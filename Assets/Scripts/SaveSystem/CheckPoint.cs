using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : TriggerObject
{
    [SerializeField]
    private string sceneName;

    private void Start()
    {
        onTrigger.AddListener(Save);
    }

    private async void Save()
    {
        bool success = await SaveManager.SaveAsync(sceneName, true);
        if (!success) Debug.LogError("SaveAsync failed");
    }
}
