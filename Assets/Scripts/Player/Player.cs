using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private int _damage;
    [SerializeField] private float _attackRange = 0.95f;

    public event UnityAction<int> HealthChange;

    private int _maxHealth = 100;
    private int _minHealth = 0;

    public int CurrentHealth => _health;
    public int MaxHealth => _maxHealth;

    private void Start()
    {
        _health = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        _health = Mathf.Clamp(_health - damage, _minHealth, _maxHealth);
        HealthChange?.Invoke(_health);
    }

    public void Healh(int healhCount)
    {
        _health = Mathf.Clamp(_health + healhCount, _minHealth, _maxHealth);
    }

    private void Attack()
    {
        Collider2D[] detectEnemies = Physics2D.OverlapCircleAll(transform.position, _attackRange);

        foreach (Collider2D detectEnemy in detectEnemies)
            if (detectEnemy.TryGetComponent(out Enemy enemy))
                enemy.TakeDamage(_damage);
    }
}
