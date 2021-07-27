using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaverObject : InteractObjects
{
    [SerializeField]
    private SavePanel savePanel;

    [SerializeField]
    private string sceneName;

    private DebutDialog debut;

    protected override void Awake()
    {
        base.Awake();
        if (TryGetComponent(out debut))
        {
            //³õ´Îµ÷²é
            debut.DialogEnded += ShowSave;
        }
    }

    public override void Interact()
    {
        if (debut != null)
            debut.StartDialog();
        else
            ShowSave();
        //SaveManager.Save(sceneName);
    }

    private void ShowSave()
    {
        savePanel.StartSave(sceneName);
    }
}
