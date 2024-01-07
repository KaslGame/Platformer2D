using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyChasing))]
public class EnemyPatrolling : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Vector2 _startPosition;
    private Vector2 _targetPostion;

    private EnemyChasing _enemyChasing;

    private float _stopDistance = 0;
    private bool _isPlayerDetected = false;

    private void Start()
    {
        _enemyChasing = GetComponent<EnemyChasing>();
        _startPosition = transform.position;
    }

    private void OnEnable()
    {
        _enemyChasing.ChasingComplete += OnChasingComplete;
    }

    private void OnDisable()
    {
        _enemyChasing.ChasingComplete -= OnChasingComplete;
    }

    private void OnChasingComplete(bool statusChasing)
    {
        _isPlayerDetected = statusChasing;
    }

    private void Update()
    {
        Patrolling();
    }

    private void Patrolling()
    {
        float xOffset = 3f;

        if (_isPlayerDetected == false)
            return;

        _targetPostion = new Vector2(_startPosition.x + xOffset, _startPosition.y);

        if (Vector2.Distance(transform.position, _targetPostion) > _stopDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, _targetPostion, _speed * Time.deltaTime);
            print("Ползу");
        }
        else
        {
            _targetPostion = new Vector2(_startPosition.x + xOffset * -1, _startPosition.y);
            Flip();
        }
    }

    private void Flip()
    {
        var direction = _targetPostion.x - transform.position.x;

        if (direction > 0)
            transform.rotation = Quaternion.identity;
        else if (direction < 0)
            transform.rotation = Quaternion.Euler(0, 180, 0);
    }
}
