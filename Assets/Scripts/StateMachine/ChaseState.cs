using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State
{
    private readonly float AttackInterval = 1f;

    private float AttackTimer = 0;

    public ChaseState(FSM fsm, Enemy enemy) : base(fsm, enemy)
    {
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
        if (AttackTimer > 0)
            AttackTimer -= Time.deltaTime;
        if (enemy.Target == null || enemy.IsDead || enemy.Target.IsDead)
        {
            fsm.TransitState(StateType.Idle);
            return;
        }
        if ((enemy.transform.position - enemy.Target.transform.position).sqrMagnitude
            < enemy.data.ShortAtkRangeSqr)
        {
            if (AttackTimer <= 0)
            {
                AttackTimer = AttackInterval;
                fsm.TransitState(StateType.Attack);
                return;
            }
            return;
        }
        enemy.Follow();
        
    }
}
