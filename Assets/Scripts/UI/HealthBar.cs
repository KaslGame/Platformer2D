using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public abstract class HealthBar : MonoBehaviour
{
    [SerializeField] protected Health Health;

    protected Slider Slider;

    private void Awake()
    {
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
