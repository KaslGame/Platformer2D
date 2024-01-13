using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SmoothHealthBar : HealthBar
{
    [SerializeField] private float _smoothSpeed = 0.05f;

    private Coroutine _coroutine;

    protected override void OnHealthChanged(int health, int oldHealth)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(HealthReduction(health));
    }

    private IEnumerator HealthReduction(float targetValue)
    {
        while (Slider.value != targetValue)
        {
            Slider.value = Mathf.MoveTowards(Slider.value, targetValue, Time.deltaTime / _smoothSpeed);
            yield return null;
        }
    }
}
