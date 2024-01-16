using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Health))]
public class Enemy : MonoBehaviour
{
    private readonly int AttackTrigger = Animator.StringToHash("Attack");
    private readonly int HurtTrigger = Animator.StringToHash("Hurt");
    private readonly int DieTrigger = Animator.StringToHash("Die");

    [Header("Gameplay Stats")]
    [SerializeField] private int _damage;
    [Header("Need Links")]
    [SerializeField] private EnemyScanner _enemyScanner;
    [SerializeField] private EnemyAttackScanner _enemyAttackScanner;


    private EnemyStateMachine _enemyStateMachine;
    private Animator _animator;
    private Player _currentPlayer;
    private Health _health;

    private float _attackRange = 0.85f;

    private bool _attackStatus = false;

    private void Awake()
    {
        _enemyStateMachine = new EnemyStateMachine(this);
        _enemyStateMachine.EnterIn<PatrollingState>();
        _animator = GetComponent<Animator>();
        _health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        _enemyScanner.PlayerDetect += OnPlayerDetect;
        _enemyAttackScanner.PlayerStayAttackRange += OnPlayerStayAttackRange;
        _health.HealthChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        _enemyScanner.PlayerDetect -= OnPlayerDetect;
        _enemyAttackScanner.PlayerStayAttackRange -= OnPlayerStayAttackRange;
        _health.HealthChanged -= OnHealthChanged;
    }

    private void Update()
    {
       _enemyStateMachine.Update();
    }

    private void OnHealthChanged(int health, int oldHealth)
    {
        if (oldHealth > health)
            _animator.SetTrigger(HurtTrigger);
    }

    private void OnPlayerDetect(Player player, bool playerEnter)
    {
        if (_attackStatus)
            return;

        if (playerEnter)
        {
            _enemyStateMachine.EnterIn<ChasingState>();
            _enemyStateMachine.SetPlayer(player);
        }
        else
        {
            _enemyStateMachine.EnterIn<PatrollingState>();
        }
    }

    private void TryDie()
    {
        if (_health.CurrentHealth <= 0)
            _animator.SetTrigger(DieTrigger);
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void OnPlayerStayAttackRange(Player player)
    {
        if (_attackStatus)
            return;

        _animator.SetTrigger(AttackTrigger);
        _currentPlayer = player;
    }

    private void Attack()
    {
        Collider2D[] detectPlayers = Physics2D.OverlapCircleAll(transform.position, _attackRange);

        foreach (Collider2D detectPlayer in detectPlayers)
            if (detectPlayer.TryGetComponent(out Player player))
                _currentPlayer.GetComponent<Health>().TakeDamage(_damage);

        _currentPlayer = null;
    }

    private void ChangeAttackStatus()
    {
        if (_attackStatus)
            _attackStatus = false;
        else
            _attackStatus = true;
    }
}
