using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : TriggerObject
{
    [SerializeField]
    private string[] dialog;

    private void Start()
    {
        onTrigger.AddListener(ShowDialog);
    }

    private void ShowDialog()
    {
        GameManager.Instance.player.StopAction?.Invoke();
        GameManager.Instance.StartDialogue(dialog);
    }
}
