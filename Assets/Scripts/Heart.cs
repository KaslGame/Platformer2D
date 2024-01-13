using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        int healhCount = 25;

        if (collision.gameObject.TryGetComponent(out Player player))
        {
            Health playerHealth = player.GetComponent<Health>();

            if (playerHealth.CurrentHealth < playerHealth.MaxHealth)
            {
                playerHealth.Healh(healhCount);
                Destroy(gameObject);
            }
        }
    }
}
