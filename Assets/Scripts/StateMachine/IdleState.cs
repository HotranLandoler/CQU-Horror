using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public IdleState(FSM fsm, Enemy enemy) : base(fsm, enemy)
    {
    }

    public override void OnEnter()
    {

    }

    public override void OnExit()
    {
        
    }

    public override void OnUpdate()
    {
        if (GameManager.Instance.CurGameMode == GameMode.Gameplay)
        {
            if (enemy.target != null && !enemy.IsDead && !enemy.target.IsDead)
                fsm.TransitState(StateType.Chase);
        }
        
    }
}
