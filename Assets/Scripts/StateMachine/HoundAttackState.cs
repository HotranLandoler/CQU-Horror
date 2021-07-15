using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoundAttackState : State
{
    public HoundAttackState(FSM fsm, Enemy enemy) : base(fsm, enemy)
    {
    }

    public override void OnEnter()
    {
        enemy.PlayAttackSound();
    }

    public override void OnExit()
    {
        
    }

    public override void OnUpdate()
    {
        if (enemy.target == null)
        {
            fsm.TransitState(StateType.Idle);
            return;
        }  
        var dir = enemy.target.transform.position - enemy.transform.position;
        //facePlayer
        enemy.SetDirection(dir);
        enemy.Attack();
        if (GameManager.Instance.CurGameMode != GameMode.Gameplay)
        {
            fsm.TransitState(StateType.Idle);
            return;
        }
        if (dir.sqrMagnitude
            > enemy.attackRange)
        {
            fsm.TransitState(StateType.Chase);
        }
    }
}
