using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Vampirism : MonoBehaviour
{
    private readonly int TurnTrigger = Animator.StringToHash("TurnZone");
    private readonly int CloseTrigger = Animator.StringToHash("CloseZone");

    [SerializeField] private int _attackRange;
    [SerializeField] private int _damage;
    [SerializeField] private float _duration;
    [SerializeField] private Animator _zone;

    private Coroutine _coroutine;
    private Health _playerHealth;

    private void Awake()
    {
        _playerHealth = GetComponent<Health>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            TryStartBloodSucking();
    }

    private void TryStartBloodSucking()
    {
        if (_coroutine == null)
           _coroutine = StartCoroutine(BloodSucking(_duration));
    }

    private IEnumerator BloodSucking(float duration)
    {
        var time = new WaitForSeconds(1f);
        int expendTime = 0;

        _zone.SetTrigger(TurnTrigger);

        while (expendTime <= duration)
        {
            Enemy enemy = GetNearest();

            if (enemy != null)
            {
                if (enemy.TryGetComponent(out Health health))
                {
                    health.TakeDamage(_damage);
                    _playerHealth.Healh(_damage);
                }
            }

            expendTime++;

            yield return time;
        }

        _coroutine = null;
        _zone.SetTrigger(CloseTrigger);
    }

    private Enemy GetNearest()
    {
        Enemy nerarestEnemy = null;

        float closestDistance = _attackRange;

        foreach (Enemy enemy in GetEnemies())
        {
            float distanceToEnemy = Vector3.Distance(enemy.transform.position, transform.position);
            if (distanceToEnemy < closestDistance)
            {
                closestDistance = distanceToEnemy;
                nerarestEnemy = enemy;
            }
        }

        return nerarestEnemy;
    }

    private List<Enemy> GetEnemies()
    {
        List<Enemy> findedEnemies = new List<Enemy>();
        Collider2D[] detectEnemies = Physics2D.OverlapCircleAll(transform.position, _attackRange);

        foreach (Collider2D detectEnemy in detectEnemies)
            if (detectEnemy.TryGetComponent(out Enemy enemy))
                findedEnemies.Add(enemy);

        return findedEnemies;
    }
}
