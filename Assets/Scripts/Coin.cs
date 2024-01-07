using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        int oneCoin = 1;

        if (collision.gameObject.TryGetComponent(out Wallet wallet))
        {
            Destroy(gameObject);
            wallet.AddMoney(oneCoin);
        }
    }
}
