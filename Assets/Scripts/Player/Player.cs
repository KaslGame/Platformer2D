using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Health))]
public class Player : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _attackRange = 0.95f;

    private void Attack()
    {
        Collider2D[] detectEnemies = Physics2D.OverlapCircleAll(transform.position, _attackRange);

        foreach (Collider2D detectEnemy in detectEnemies)
            if (detectEnemy.TryGetComponent(out Enemy enemy))
                enemy.GetComponent<Health>().TakeDamage(_damage);
    }
}
