using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BusinessState : StateBase
{
    protected Business _business;

    public override void Initialize(StateMachineBase stateMachine, MonoBehaviour main)
    {
        base.Initialize(stateMachine, main);
        _business = main as Business;
    }
}
