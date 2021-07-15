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

    private void Save()
    {
        SaveManager.SaveAsync(sceneName, true);
    }
}
