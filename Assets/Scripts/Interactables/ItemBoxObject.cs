using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBoxObject : InteractObjects
{
    private DebutDialog debut;

    [SerializeField]
    private Animator boxAnimator;

    [SerializeField]
    private ItemBox boxUi;

    [SerializeField]
    private AudioClip openSound;

    private void Start()
    {
        if (TryGetComponent(out debut))
        {
            //³õ´Îµ÷²é
            debut.DialogEnded += OpenBox;
        }
        boxUi.Closed += () => boxAnimator.SetTrigger("Close");
    }

    public override void Interact()
    {
        if (debut) debut.StartDialog();
        else OpenBox();
    }

    private void OpenBox()
    {
        if (openSound) AudioManager.Instance.PlaySound(openSound);
        boxAnimator.SetTrigger("Open");
        boxUi.Open();
    }
}
