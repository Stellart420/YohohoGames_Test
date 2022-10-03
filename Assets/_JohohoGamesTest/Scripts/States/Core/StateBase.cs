using UnityEngine;

public abstract class StateBase : MonoBehaviour
{
    public StateMachineBase SM { get; protected set; }

    public virtual void Initialize(StateMachineBase stateMachine, MonoBehaviour main)
    {
        SM = stateMachine;
    }

    public abstract void OnStateEnter();

    public abstract void OnStateExit();

    public abstract void Loop();
}
