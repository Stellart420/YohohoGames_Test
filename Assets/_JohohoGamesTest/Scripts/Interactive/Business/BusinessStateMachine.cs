using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BusinessStateMachine : StateMachineBase
{
    [SerializeField] private List<BusinessState> _states = new List<BusinessState>();
    [SerializeField] private BusinessState _initState;

    public void Init(Business business)
    {
        _states.ForEach(state => state.Initialize(this, business));
        if (_initState)
            ChangeState(_initState);
    }

    public void NextState()
    {
        var index = _states.IndexOf(CurrentState as BusinessState);
        index++;
        if (_states.Count > index)
        {
            var nextState = _states[index];
            ChangeState(nextState);
        }
    }

    public override StateBase GetState<T>(T state)
    {
        return _states.FirstOrDefault(s => s.GetType() == state);
    }
}
