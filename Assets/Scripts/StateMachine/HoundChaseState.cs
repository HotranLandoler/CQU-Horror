using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoundChaseState : State
{
    public HoundChaseState(FSM fsm, Enemy enemy) : base(fsm, enemy)
    {
    }

    public override void OnEnter()
    {
        enemy.StopFollow(false);
    }

    public override void OnExit()
    {
        enemy.StopFollow(true);
    }

    public override void OnUpdate()
    { 
        if (GameManager.Instance.CurGameMode != GameMode.Gameplay || enemy.target == null)
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
