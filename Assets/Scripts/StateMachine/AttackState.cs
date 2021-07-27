using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    float time;
    public AttackState(FSM fsm, Enemy enemy) : base(fsm, enemy)
    {
    }

    public override void OnEnter()
    {
        time = 0;
        enemy.PlayAttackSound();
        enemy.animator.SetTrigger("Attack");
        enemy.Nav.speed = enemy.data.FastSpeed;
        enemy.Nav.SetDestination(enemy.Target.transform.position);      
    }

    public override void OnExit()
    {
        
    }

    public override void OnUpdate()
    {
        time += Time.deltaTime;
        if (enemy.IsDead || enemy.Target == null || GameManager.Instance.CurGameMode != GameMode.Gameplay)
        {
            fsm.TransitState(StateType.Idle);
            return;
        }
        enemy.Attack();
        if (time > 1.5f)
        {
            fsm.TransitState(StateType.Chase);
        }
        //if (Vector2.SqrMagnitude(enemy.transform.position - enemy.Target.transform.position)
        //    > enemy.data.ShortAtkRangeSqr)
        //{
        //    fsm.TransitState(StateType.Chase);
        //}
    }

}
