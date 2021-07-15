using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceDetecter : InteractObjects
{
    [SerializeField]
    private Animator detecterAnimator;

    [SerializeField]
    private string phrase;

    [SerializeField]
    private AudioClip se;

    public override void Interact()
    {
        if (se != null)
            AudioManager.Instance.PlaySound(se);
        detecterAnimator.Play("FacePass");
        GameManager.Instance.StartDialogue(phrase);
        //UIManager.Instance.ShowDialogue(phraseName, false);
    }
}
