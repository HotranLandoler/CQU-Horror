using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : Door
{
    [SerializeField]
    private bool isLocked = true;

    [SerializeField]
    private Item key;

    [SerializeField]
    private AudioClip lockedSound;

    [SerializeField]
    private string[] lockedDialog;

    public override void Interact()
    {
        if (isLocked)
        {
            if (key != null && GameManager.Instance.inventory.HasItem(key) > 0)
            {
                //¿ªËø
                isLocked = false;
                //#½øÈë
            }
            else
            {
                if (lockedSound) AudioManager.Instance.PlaySound(lockedSound);
                if (lockedDialog.Length > 0)
                {
                    GameManager.Instance.StartDialogue(lockedDialog);
                }
                else
                    GameManager.Instance.StartDialogue(Game.gameStrings.LockedDoor);
            }     
        }
        else
        {
            OpenDoor();
        }
    }
}
