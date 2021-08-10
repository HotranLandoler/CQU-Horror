using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWithDialog : InteractObjects
{
    [TextArea(2,3)]
    [SerializeField]
    private string[] phraseName;

    [SerializeField]
    private AudioClip se;

    [SerializeField]
    private GameFlag setFlag;

    public override void Interact()
    {
        if (se != null)
            AudioManager.Instance.PlaySound(se);
        if (setFlag && !setFlag.Has())
            setFlag.Set();
        GameManager.Instance.StartDialogue(phraseName);
        //UIManager.Instance.ShowDialogue(phraseName, false);
        //if (itemGet != null)
        //{
        //    GameManager.Instance.AddItem(itemGet, itemNum);
        //    Destroy(gameObject);
        //}      
    }
}
