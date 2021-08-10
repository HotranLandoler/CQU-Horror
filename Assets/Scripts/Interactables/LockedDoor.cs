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
    private AudioClip unlockSound;

    [SerializeField]
    private string[] lockedDialog;

    [SerializeField]
    private GameFlag unlockedFlag;

    private void Start()
    {
        if (unlockedFlag && unlockedFlag.Has())
            isLocked = false;
    }

    public override void Interact()
    {
        if (isLocked)
        {
            if (key != null && GameManager.Instance.inventory.HasItem(key) > 0)
            {
                //开锁
                if (unlockSound) AudioManager.Instance.PlaySound(unlockSound);
                isLocked = false;
                unlockedFlag.Set();
                GameManager.Instance.StartDialogue($"使用 {key.Name} 打开了锁。");
                GameManager.Instance.inventory.RemoveItem(key);
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
