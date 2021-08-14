using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LengAttackState : State
{
    private float specialChance;
    private AnimatorStateInfo info;
    //float time;
    public LengAttackState(FSM fsm, Enemy enemy, float specialChance) : base(fsm, enemy)
    {
        this.specialChance = specialChance;
    }

    public override void OnEnter()
    {
        //enemy.SetDirection(enemy.Target.transform.position - enemy.transform.position);
        if (enemy.SpecialTimer[0] <= 0)
        {
            //���������ж�
            if (Random.Range(0f,1f) <= specialChance)
            {
                //�����ж�
                enemy.ResetTimer(0);
                enemy.PrepareHeavyAttack();
                enemy.animator.SetBool("Special", true);
                return;
            }
        }
        enemy.PrepareLightAttack();
        enemy.PlayAttackSound();
        enemy.animator.SetBool("Attack", true);
        //enemy.Nav.speed = enemy.data.FastSpeed;
        //enemy.Nav.SetDestination(enemy.Target.transform.position);      
    }

    public override void OnExit()
    {
        enemy.animator.SetBool("Attack", false);
        enemy.animator.SetBool("Special", false);
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