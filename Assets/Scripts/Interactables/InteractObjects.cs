using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public abstract class InteractObjects : MonoBehaviour, IInteractable
{
    [SerializeField]
    protected Animator tipAnimator;

    protected Animator _animator;

    private Collider2D triggerCollider;

    // Start is called before the first frame update
    protected virtual void Awake()
    {
        _animator = GetComponent<Animator>();
        triggerCollider = GetComponent<Collider2D>();
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

    /// <summary>
    /// 手动关闭互动
    /// </summary>
    protected void Deactivate()
    {
        triggerCollider.enabled = false;
    }

    public abstract void Interact();
}
