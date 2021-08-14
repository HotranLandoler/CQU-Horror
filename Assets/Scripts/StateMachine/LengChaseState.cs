using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LengChaseState : State
{
    private float attackInterval;

    private float attackTimer = 0;

    private float distance;

    public LengChaseState(FSM fsm, Enemy enemy, float attackInterval) : base(fsm, enemy)
    {
        this.attackInterval = attackInterval;
    }

    public override void OnEnter()
    {
        //Debug.Log("Chase");
        enemy.SetDirection(enemy.Target.transform.position - enemy.transform.position);
        enemy.Nav.speed = enemy.data.NormSpeed;
        enemy.StopFollow(false);
    }

    public override void OnExit()
    {
        //enemy.Nav.SetDestination(enemy.transform.position);
        enemy.animator.SetBool("Move", false);
        enemy.StopFollow(true);
    }

    public override void OnUpdate()
    {
        if (attackTimer > 0)
            attackTimer -= Time.deltaTime;
        if (enemy.Target == null || enemy.IsDead || enemy.Target.IsDead)
        {
            fsm.TransitState(StateType.Idle);
            return;
        }
        distance = (enemy.transform.position - enemy.Target.transform.position).sqrMagnitude;
        if (distance <= enemy.data.ShortAtkRangeSqr)
        {
            if (attackTimer <= 0)
            {
                attackTimer = attackInterval;
                fsm.TransitState(StateType.Attack);
                return;
            }
            return;
        }
        else if (distance <= enemy.data.LongAtkRangeSqr )
        {
            //ÔÚÔ¶³Ì·¶Î§ÄÚ
            if (attackTimer <= 0 && enemy.SpecialTimer[1] <= 0)
            {
                enemy.ResetTimer(1);
                attackTimer = attackInterval;
                fsm.TransitState(StateType.Shoot);
                return;
            }
        }
        enemy.Follow();

    }
}
