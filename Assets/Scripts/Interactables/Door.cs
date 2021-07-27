using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : SceneTravelBase, IInteractable
{
    private Animator animator;

    [SerializeField]
    private AudioClip openSound;

    [SerializeField]
    private MeshRenderer nameText;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public virtual void Interact()
    {
        OpenDoor();
    }

    protected void OpenDoor()
    {
        if (openSound) AudioManager.Instance.PlaySound(openSound);
        animator.SetTrigger("Open");
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
                //nameText.SetActive(true);
                nameText.enabled = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().interactObjs.Remove(this);
            //nameText.SetActive(false);
            nameText.enabled = false;
        }
    }
}
