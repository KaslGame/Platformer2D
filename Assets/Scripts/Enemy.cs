using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _stopDistance;
    [SerializeField] private float _speed;

    private Vector2 _targetPostion;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        ResetTargetPosition();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (Vector2.Distance(transform.position, _targetPostion) > _stopDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, _targetPostion, _speed * Time.deltaTime);
        }
        else
        {
            _targetPostion = new Vector2(_targetPostion.x * -1, _targetPostion.y);
            Flip();
        }
    }

    private void Flip()
    {
        _spriteRenderer.flipX = !_spriteRenderer.flipX;
    }

    private void ResetTargetPosition()
    {
        _targetPostion = new Vector2(transform.position.x + 3, transform.position.y);
    }
}
