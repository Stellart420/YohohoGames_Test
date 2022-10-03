using System;
using UnityEngine;

public abstract class StateMachineBase : MonoBehaviour
{
    public StateBase CurrentState { get; private set; }

    public void ChangeState<T>(T state_type) where T : Type
    {
        var state = GetState(state_type);
        ChangeState(state);
    }

    public void ChangeState(StateBase state)
    {
        if (CurrentState != null)
        {
            CurrentState.OnStateExit();
        }

        CurrentState = state;

        if (CurrentState)
            CurrentState.OnStateEnter();

    }

    protected virtual void Update()
    {
        if (CurrentState != null)
            CurrentState.Loop();
    }

    public abstract StateBase GetState<T>(T state) where T : Type;
}
