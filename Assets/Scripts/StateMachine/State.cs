using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected FSM fsm;
    protected Enemy enemy;
    public State(FSM fsm, Enemy enemy)
    {
        this.fsm = fsm;
        this.enemy = enemy;
    }

    public abstract void OnEnter();
    public abstract void OnUpdate();
    public abstract void OnExit();
}
