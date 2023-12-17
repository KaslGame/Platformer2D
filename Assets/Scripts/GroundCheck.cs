using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GroundCheck : MonoBehaviour
{
    public event UnityAction<bool> StateOnGroundChange;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Ground _))
        {
            StateOnGroundChange?.Invoke(true);
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Ground _))
            StateOnGroundChange?.Invoke(false);
    }
}
