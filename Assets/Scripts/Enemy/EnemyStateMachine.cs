using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine
{
    private Dictionary<Type, IEnemyState> _states;
    private IEnemyState _currentState;

    public EnemyStateMachine(Enemy enemy)
    {
        _states = new Dictionary<Type, IEnemyState>()
        {
            [typeof(PatrollingState)] = new PatrollingState(this, enemy.transform),
            [typeof(ChasingState)] = new ChasingState(this, enemy.transform)
        };

        _currentState = _states[typeof(PatrollingState)];
    }

    public void EnterIn<TState>() where TState : IEnemyState
    {
        if (_states.TryGetValue(typeof(TState), out IEnemyState state))
            _currentState = state;
    }

    public void Update()
    {
        _currentState.Update();
    }

    public void SetPlayer(Player player)
    {
        _currentState.SetPlayer(player);
    }
}
