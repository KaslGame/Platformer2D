using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(EnemyChasing))]
public class EnemyChasing : MonoBehaviour
{
    [SerializeField] private float _speed;

    public event UnityAction<bool> ChasingComplete;

    private EnemyScanner _enemyScanner;
    private Player _target;
    private Vector2 _targetPostion;

    private float _stopDistance = 1f;

    private void Update()
    {
        Chasing();
    }

    //private void OnEnable()
    //{
    //    _enemyScanner.PlayerDetect += OnPlayerDetect;
    //}

    //private void OnDisable()
    //{
    //    _enemyScanner.PlayerDetect -= OnPlayerDetect;
    //}

    private void OnPlayerDetect(Player player)
    {
        _target = player;
    }

    private void Chasing()
    {
        if (_target == null)
            return;

        ChasingComplete?.Invoke(false);

        _targetPostion = _target.transform.position;

        if (Vector2.Distance(transform.position, _targetPostion) > _stopDistance)
            transform.position = Vector2.MoveTowards(transform.position, _targetPostion, _speed * Time.deltaTime);
    }
}
