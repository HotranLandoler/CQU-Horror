using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hound : Enemy
{
    private EnemyAttack[] attackShapes;

    protected override void Start()
    {
        base.Start();
        foreach (var item in attackShapes)
        {
            item.damage = Damage();
        }
    }

    private void Update()
    {
        if (GameManager.Instance.player != null && GameManager.Instance.player.IsDead == false)
        {
            Target = GameManager.Instance.player;
        }
        else
            Target = null;
    }

    public override void Follow()
    {
        if (GameManager.Instance.CurGameMode == GameMode.Gameplay )
            //GameManager.Instance.CurGameMode == GameMode.Items)
        {
            Nav.destination = GameManager.Instance.player.transform.position;
            //move = (GameManager.Instance.player.transform.position - transform.position).normalized;
            move = Nav.velocity;
            animator.SetBool("Move", true);
            animator.SetFloat("MoveX", move.x);
            animator.SetFloat("MoveY", move.y);
        }      
    }

    private void FixedUpdate()
    {
        //trueSpeed = moveSpeed;
        //if (move.x != 0 && move.y != 0)
        //    trueSpeed *= speedMod;
        //rb.velocity = move * trueSpeed;
    }

    public override int Damage()
    {
        return 35;
    }

    public override void Attack()
    {
        animator.SetTrigger("Attack");
        //foreach (var item in attackShapes)
        //{
        //    item.gameObject.SetActive(false);
        //}
        //attackShapes[GameManager.Instance.NormalDirInt(move)].gameObject.SetActive(true);
    }

    public void ShowAttackBox(int dir)
    {
        if (dir == Direction.NormalDirInt(move))
            attackShapes[dir].gameObject.SetActive(true);
    }

    public void HideAttackBox()
    {
        foreach (var item in attackShapes)
        {
            item.gameObject.SetActive(false);
        }
        //attackShapes[dir].gameObject.SetActive(false);
    }
}
