using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy
{
    [SerializeField]
    private EnemyAttack attackShape;

    protected override void Start()
    {
        base.Start();
        attackShape.damage = Damage();
    }

    public override void Attack()
    {
        //animator.SetTrigger("Attack");
    }

    public override int Damage()
    {
        return 5;
    }

    public override void Follow()
    {
        nav.destination = target.transform.position;
        //move = (GameManager.Instance.player.transform.position - transform.position).normalized;
        move = nav.velocity;
        animator.SetBool("Move", true);
        Vector3 scale;
        if (move.x < 0)
            scale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        else
            scale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        transform.localScale = scale;
    }

    public override void Die()
    {
        GameManager.Instance.gameVariables.SetFlag(flag);
        StopFollow(true);
        IsDead = true;
        //animator.SetTrigger("Die");
        collider2d.enabled = false;
        Destroy(gameObject);
    }

    //public void ShowAttackBox(int show)
    //{
    //    if (show == 1)
    //        attackShape.gameObject.SetActive(true);
    //    else
    //        attackShape.gameObject.SetActive(false);
    //}
}
