using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LengFSM : FSM
{
    [SerializeField]
    private float specialChance;

    [SerializeField]
    private float attackInterval = 1f;

    public override void Init()
    {
        //states.Add(StateType.Idle, new IdleState(this, enemy));
        states.Add(StateType.Idle, new IdleState(this, enemy));
        states.Add(StateType.Chase, new LengChaseState(this, enemy, attackInterval));
        states.Add(StateType.Attack, new LengAttackState(this, enemy, specialChance));
        states.Add(StateType.Shoot, new ShootState(this, enemy, GetComponent<EnemyShoot>()));
        TransitState(StateType.Idle);
    }
}
