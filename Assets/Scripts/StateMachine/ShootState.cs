using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootState : State
{
    private EnemyShoot shooter;

    private float distance;

    private float timer;

    private int count;
    public ShootState(FSM fsm, Enemy enemy, EnemyShoot shoot) : base(fsm, enemy)
    {
        shooter = shoot;
    }

    public override void OnEnter()
    {
        timer = 0;
        count = 0;
        enemy.animator.SetBool("Shoot", true);
    }

    public override void OnExit()
    {
        enemy.animator.SetBool("Shoot", false);
    }

    public override void OnUpdate()
    {
        if (timer > 0) timer -= Time.deltaTime;
        //time += Time.deltaTime;
        if (enemy.IsDead || enemy.Target == null || GameManager.Instance.CurGameMode != GameMode.Gameplay)
        {
            fsm.TransitState(StateType.Idle);
            return;
        }
        distance = (enemy.transform.position - enemy.Target.transform.position).sqrMagnitude;
        if (distance > enemy.data.LongAtkRangeSqr || count >= shooter.shootCount)
        {
            fsm.TransitState(StateType.Chase);
            return;
        }
        enemy.SetDirection(enemy.Target.transform.position - enemy.transform.position);
        if (timer <= 0)
        {
            shooter.Shoot();
            count++;
            timer = shooter.shootInterval;
        }
    }
}
