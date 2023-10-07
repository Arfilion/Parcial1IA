using  System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachine
{
    State _currentState;
    Dictionary<EnemyActions, State> _allStates = new Dictionary<EnemyActions, State>();

    public void Update()
    {
       
        _currentState?.OnUpdate(); 
    }

    public void AddState(EnemyActions name, State state)
    {
        if (!_allStates.ContainsKey(name))
        {
            _allStates.Add(name, state);
            state.fsm = this;
        }
        else
        {
            _allStates[name] = state;
        }
    }

    public void ChangeState(EnemyActions name)
    {
        _currentState?.OnExit();
        if (_allStates.ContainsKey(name)) _currentState = _allStates[name];
        _currentState?.OnEnter();
    }
}
