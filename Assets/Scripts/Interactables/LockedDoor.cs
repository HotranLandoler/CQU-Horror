using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : Door
{
    [SerializeField]
    private bool isLocked = true;

    [SerializeField]
    private int keyId = -1;

    [SerializeField]
    private AudioClip lockedSound;

    public override void Interact()
    {
        if (isLocked)
        {
            if (keyId != -1 && GameManager.Instance.inventory.HasItem(keyId) > 0)
            {
                //¿ªËø
                isLocked = false;
                //#½øÈë
            }
            else
            {
                if (lockedSound) AudioManager.Instance.PlaySound(lockedSound);
                UIManager.Instance.ShowDialogue(Game.gameStrings.LockedDoor, false);
            }     
        }
        else
        {
            OpenDoor();
        }
    }
}
