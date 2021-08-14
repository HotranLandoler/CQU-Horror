using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    private AnimatorStateInfo info;
    //float time;
    public AttackState(FSM fsm, Enemy enemy) : base(fsm, enemy)
    {
    }

    public override void OnEnter()
    {
        //time = 0;
        //enemy.SetDirection(enemy.Target.transform.position - enemy.transform.position);
        enemy.PrepareLightAttack();
        enemy.PlayAttackSound();
        enemy.animator.SetBool("Attack",true);
        //enemy.Nav.speed = enemy.data.FastSpeed;
        //enemy.Nav.SetDestination(enemy.Target.transform.position);      
    }

    public override void OnExit()
    {
        enemy.animator.SetBool("Attack", false);
        enemy.HideAttackBox();
    }

    public override void OnUpdate()
    {
//time += Time.deltaTime;
        if (enemy.IsDead || enemy.Target == null || GameManager.Instance.CurGameMode != GameMode.Gameplay)
        {
            fsm.TransitState(StateType.Idle);
            return;
        }
        info = enemy.animator.GetCurrentAnimatorStateInfo(0);
        //enemy.Attack();
        if (info.normalizedTime >= 0.95f)
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
