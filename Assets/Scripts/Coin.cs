using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        int oneCoin = 1;

        if (collision.gameObject.TryGetComponent(out PlayerMovement _))
        {
            Wallet _wallet = collision.gameObject.GetComponent<Wallet>();

            Destroy(gameObject);
            _wallet.AddMoney(oneCoin);
        }
    }
}
