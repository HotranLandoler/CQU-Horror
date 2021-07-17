using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public abstract class InteractObjects : MonoBehaviour, IInteractable
{
    [SerializeField]
    private Animator tipAnimator;

    private Animator _animator;

    // Start is called before the first frame update
    protected void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var player = collision.GetComponent<Player>();
            if (!player.interactObjs.Contains(this))               
                player.interactObjs.Add(this);
            _animator.SetBool("FadeIn", true);
            tipAnimator.SetBool("MovFadeIn", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().interactObjs.Remove(this);
            _animator.SetBool("FadeIn", false);
            tipAnimator.SetBool("MovFadeIn", false);
        }
    }

    public abstract void Interact();
}
