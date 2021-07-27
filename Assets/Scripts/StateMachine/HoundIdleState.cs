using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoundIdleState : State
{
    public HoundIdleState(FSM fsm, Enemy enemy) : base(fsm, enemy)
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
        if (GameManager.Instance.CurGameMode == GameMode.Gameplay && enemy.Target != null)
            fsm.TransitState(StateType.Chase);
    }
}
