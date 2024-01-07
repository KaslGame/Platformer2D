using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CircleCollider2D))]
public class EnemyAttackScanner : MonoBehaviour
{
    [SerializeField] private float _attackRange;

    public event UnityAction<Player> PlayerStayAttackRange;

    private CircleCollider2D _circleCollider2D;

    private void Awake()
    {
        _circleCollider2D = GetComponent<CircleCollider2D>();
        _circleCollider2D.radius = _attackRange;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
            PlayerStayAttackRange?.Invoke(player);
    }
}
