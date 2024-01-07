using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CircleCollider2D))]
public class EnemyScanner : MonoBehaviour
{
    [SerializeField] private float _detectionRange;

    public event UnityAction<Player, bool> PlayerDetect;

    private CircleCollider2D _circleCollider2D;

    private void Awake()
    {
        _circleCollider2D = GetComponent<CircleCollider2D>();
        _circleCollider2D.radius = _detectionRange;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
            PlayerDetect?.Invoke(player, true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
            PlayerDetect?.Invoke(player, false);
    }
}
