using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderFSM : FSM
{
    public override void Init()
    {
        //states.Add(StateType.Idle, new IdleState(this, enemy));
        states.Add(StateType.Idle, new IdleState(this, enemy));
        states.Add(StateType.Chase, new ChaseState(this, enemy));
        TransitState(StateType.Idle);
    }
}
