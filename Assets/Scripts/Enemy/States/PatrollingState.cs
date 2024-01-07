using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PatrollingState : IEnemyState
{
    private readonly EnemyStateMachine _enemyStateMachine;

    private float _speed = 2f;

    private Vector2 _targetPosition;
    private Vector2 _startPosition;
    private Transform _transform;

    private float _xOffset = 3f;
    private bool _movingTowardsTarget = true;

    public PatrollingState(EnemyStateMachine enemyStateMachine, Transform transform)
    {
        _enemyStateMachine = enemyStateMachine;
        _transform = transform;
        _startPosition = _transform.position;
        _targetPosition = new Vector2(_startPosition.x + _xOffset, _transform.position.y);
    }

    public void Update()
    {
        Patrolling();
    }

    public void SetPlayer(Player player) { }

    private void Patrolling()
    {
        Vector2 thisPosition = new Vector2(_transform.position.x, _transform.position.y);

        if (_movingTowardsTarget)
        {
            _transform.position = Vector2.MoveTowards(_transform.position, _targetPosition, _speed * Time.deltaTime);

            if (thisPosition == _targetPosition)
            {
                _movingTowardsTarget = false;
                Flip();
            }
        }
        else
        {
            _transform.position = Vector2.MoveTowards(_transform.position, _startPosition, _speed * Time.deltaTime);

            if (thisPosition == _startPosition)
            {
                _movingTowardsTarget = true;
                Flip();
            }
        }
    }

    private void Flip()
    {
        if (_movingTowardsTarget)
            _transform.rotation = Quaternion.identity;
        else
            _transform.rotation = Quaternion.Euler(0, 180, 0);
    }
}
