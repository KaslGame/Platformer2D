using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class HealthTextView : HealthBar
{
    private TMP_Text _healthText;

    private void Awake()
    {
        _healthText = GetComponent<TMP_Text>();
    }

    protected override void OnHealthChanged(int health, int oldHealth)
    {
        _healthText.text = $"{health}/{oldHealth}";
    }
}
