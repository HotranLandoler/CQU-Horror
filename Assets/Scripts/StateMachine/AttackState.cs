using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    public AttackState(FSM fsm, Enemy enemy) : base(fsm, enemy)
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
        if (enemy.IsDead || enemy.target == null || GameManager.Instance.CurGameMode != GameMode.Gameplay)
        {
            fsm.TransitState(StateType.Idle);
            return;
        }
        enemy.Attack();
        if (Vector2.SqrMagnitude(enemy.transform.position - enemy.target.transform.position)
            > enemy.attackRange)
        {
            fsm.TransitState(StateType.Chase);
        }
    }

}
