using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class HealthTextView : MonoBehaviour
{
    [SerializeField] private Health _health;
    private TMP_Text _healthText;

    private void Awake()
    {
        _healthText = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        _health.HealthChanged += OnHealthChaged;
    }

    private void OnDisable()
    {
        _health.HealthChanged -= OnHealthChaged;
    }

    private void OnHealthChaged(int health, int oldHealth)
    {
        _healthText.text = $"{health}/{_health.MaxHealth}";
    }
}
