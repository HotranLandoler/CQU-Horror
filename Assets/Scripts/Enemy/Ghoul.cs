using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghoul : Enemy
{
    private EnemyAttack attackShape;

    protected override void Start()
    {
        base.Start();
        attackShape.Damage = Damage();
    }

    public override void Attack()
    {
        animator.SetTrigger("Attack");
    }

    public override int Damage()
    {
        return 20;
    }

    public override void Follow()
    {
        Nav.destination = Target.transform.position;
        //move = (GameManager.Instance.player.transform.position - transform.position).normalized;
        move = Nav.velocity;
        animator.SetBool("Move", true);
        Vector3 scale;
        if (move.x < 0)
            scale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        else
            scale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        transform.localScale = scale;
    }

    public void ShowAttackBox(int show)
    {
        if (show == 1)
            attackShape.gameObject.SetActive(true);
        else
            attackShape.gameObject.SetActive(false);
    }
}
