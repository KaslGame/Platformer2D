using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private int _health;

    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private int _minHealth = 0;

    public event UnityAction<int, int> HealthChanged;

    public int CurrentHealth => _health;
    public int MaxHealth => _maxHealth;

    private void Start()
    {
        _health = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        int oldHealth = _health;

        _health = Mathf.Clamp(_health - damage, _minHealth, _maxHealth);
        HealthChanged?.Invoke(_health, oldHealth);
    }

    public void Healh(int healhCount)
    {
        int oldHealth = _health;

        _health = Mathf.Clamp(_health + healhCount, _minHealth, _maxHealth);
        HealthChanged?.Invoke(_health, oldHealth);
    }
}
