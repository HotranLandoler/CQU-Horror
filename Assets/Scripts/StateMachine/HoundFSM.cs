using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoundFSM : FSM
{
    public override void Init()
    {
        //states.Add(StateType.Idle, new IdleState(this, enemy));
        states.Add(StateType.Idle, new HoundIdleState(this, enemy));
        states.Add(StateType.Chase, new HoundChaseState(this, enemy));
        states.Add(StateType.Attack, new HoundAttackState(this, enemy));
        TransitState(StateType.Idle);
    }
}
