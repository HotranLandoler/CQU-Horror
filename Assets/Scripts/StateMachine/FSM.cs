using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateType
{
    Idle,
    Patrol,
    Chase,
    Attack,
}

public class FSM : MonoBehaviour
{
    protected Enemy enemy;

    protected State currentState;

    protected Dictionary<StateType, State> states = new Dictionary<StateType, State>();

    private void Start()
    {
        enemy = GetComponent<Enemy>();
        Init();
    }

    private void Update()
    {
        currentState?.OnUpdate();
    }

    public void TransitState(StateType type)
    {
        Debug.Log(type.ToString());
        currentState?.OnExit();
        currentState = states[type];
        currentState.OnEnter();
    }

    public virtual void Init()
    {
        states.Add(StateType.Idle, new IdleState(this, enemy));
        states.Add(StateType.Chase, new ChaseState(this, enemy));
        states.Add(StateType.Attack, new AttackState(this, enemy));
        TransitState(StateType.Idle);
    }
}
