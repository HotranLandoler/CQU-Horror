using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaverObject : InteractObjects
{
    [SerializeField]
    private SavePanel savePanel;

    [SerializeField]
    private string sceneName;

    public override void Interact()
    {        
        savePanel.StartSave(sceneName);
        //SaveManager.Save(sceneName);
    }
}
