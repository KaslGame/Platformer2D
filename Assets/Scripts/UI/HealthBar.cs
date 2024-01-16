using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public abstract class HealthBar : MonoBehaviour
{
    protected Health Health;

    protected Slider Slider;

    private void Awake()
    {
        this.Health = GetComponentInParent<Health>();
        Slider = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        Health.HealthChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        Health.HealthChanged -= OnHealthChanged;
    }

    protected abstract void OnHealthChanged(int health, int maxHealth);
}
