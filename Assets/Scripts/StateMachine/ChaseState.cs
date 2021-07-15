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
        enemy.StopFollow(false);
    }

    public override void OnExit()
    {
        enemy.StopFollow(true);
    }

    public override void OnUpdate()
    {
        if (enemy.target == null || enemy.IsDead || enemy.target.IsDead)
        {
            fsm.TransitState(StateType.Idle);
            return;
        }
        enemy.Follow();
        if ((enemy.transform.position - GameManager.Instance.player.transform.position).sqrMagnitude
            < enemy.attackRange)
        {
            fsm.TransitState(StateType.Attack);
        }
    }
}
