using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State
{
    public ChaseState(FSM fsm, Enemy enemy) : base(fsm, enemy)
    {
    }

    public override void OnEnter()
    {
        //Debug.Log("Chase");
        enemy.Nav.speed = enemy.data.NormSpeed;
        //enemy.StopFollow(false);
    }

    public override void OnExit()
    {
        //enemy.StopFollow(true);
    }

    public override void OnUpdate()
    {
        if (enemy.Target == null || enemy.IsDead || enemy.Target.IsDead)
        {
            fsm.TransitState(StateType.Idle);
            return;
        }
        enemy.Follow();
        if ((enemy.transform.position - enemy.Target.transform.position).sqrMagnitude
            < enemy.data.LongAtkRangeSqr)
        {
            fsm.TransitState(StateType.Attack);
        }
    }
}
