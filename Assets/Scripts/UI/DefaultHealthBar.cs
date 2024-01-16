using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class DefaultHealthBar : HealthBar
{
    protected override void OnHealthChanged(int health, int maxHealth)
    {
        Slider.maxValue = maxHealth;
        Slider.value = health;
    }
}
