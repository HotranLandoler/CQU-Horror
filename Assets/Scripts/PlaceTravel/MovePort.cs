using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePort : InteractObjects
{
    [SerializeField]
    private Transform targetPos;

    [SerializeField]
    private Vector2 direction = Vector2.down;

    [SerializeField]
    private AudioClip moveSound;

    public override void Interact()
    {
        AudioManager.Instance.PlaySound(moveSound);
        GameManager.Instance.MovePlayer(targetPos.position, direction);       
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Player"))
    //    {
    //        GameManager.Instance.player.StopAction?.Invoke();
    //        collision.transform.position = targetPos.position;
    //        collision.GetComponent<Player>().SetDirection(direction);
    //    }
    //}
}
