using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingState : IEnemyState
{
    private readonly EnemyStateMachine _enemyStateMachine;

    private Transform _transform;
    private Player _player;
    private Vector2 _targetPostion;

    private float _speed = 3f;
    private float _stopDistance = 1f;

    public ChasingState(EnemyStateMachine enemyStateMachine, Transform transform)
    {
        _enemyStateMachine = enemyStateMachine;
        _transform = transform;
    }

    public void SetPlayer(Player player)
    {
        _player = player;
    }

    public void Update()
    {
        Chasing();
    }

    private void Chasing()
    {
        if (_player == null)
            return;

        _targetPostion = new Vector2(_player.transform.position.x, _transform.position.y);

        if (Vector2.Distance(_transform.position, _targetPostion) > _stopDistance)
            _transform.position = Vector2.MoveTowards(_transform.position, _targetPostion, _speed * Time.deltaTime);

        TryFlip();
    }

    private void TryFlip()
    {
        var direction = _targetPostion.x - _transform.position.x;

        if (direction > 0)
            _transform.rotation = Quaternion.identity;
        else if (direction < 0)
            _transform.rotation = Quaternion.Euler(0, 180, 0);
    }
}
