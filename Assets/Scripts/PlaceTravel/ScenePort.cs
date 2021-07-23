using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePort : SceneTravelBase, IInteractable
{
    [SerializeField]
    private Animator tipAnimator;

    public virtual void Interact()
    {
        SceneTravel();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var player = collision.GetComponent<Player>();
            if (!player.interactObjs.Contains(this))
            {
                player.interactObjs.Add(this);
                tipAnimator.SetBool("MovFadeIn", true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().interactObjs.Remove(this);
            tipAnimator.SetBool("MovFadeIn", false);
        }
    }
}
