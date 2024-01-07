using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    private readonly int AttackTrigger = Animator.StringToHash("Attack");
    private readonly int HurtTrigger = Animator.StringToHash("Hurt");
    private readonly int DieTrigger = Animator.StringToHash("Die");

    [Header("Gameplay Stats")]
    [SerializeField] private int _damage;
    [SerializeField] private int _health;
    [Header("Need Links")]
    [SerializeField] private EnemyScanner _enemyScanner;
    [SerializeField] private EnemyAttackScanner _enemyAttackScanner;

    public event UnityAction<int> HealthChange;

    private EnemyStateMachine _enemyStateMachine;
    private Animator _animator;
    private Player _currentPlayer;

    private int _minHealh = 0;
    private int _maxHealh = 20;
    private float _attackRange = 0.85f;

    private bool _attackStatus = false;

    private void Awake()
    {
        _enemyStateMachine = new EnemyStateMachine(this);
        _enemyStateMachine.EnterIn<PatrollingState>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _health = _maxHealh;
    }

    private void OnEnable()
    {
        _enemyScanner.PlayerDetect += OnPlayerDetect;
        _enemyAttackScanner.PlayerStayAttackRange += OnPlayerStayAttackRange; 
    }

    private void OnDisable()
    {
        _enemyScanner.PlayerDetect -= OnPlayerDetect;
        _enemyAttackScanner.PlayerStayAttackRange -= OnPlayerStayAttackRange;
    }

    private void Update()
    {
       _enemyStateMachine.Update();
    }

    public void TakeDamage(int damage)
    {
        _health = Mathf.Clamp(_health - damage, _minHealh, _maxHealh);
        _animator.SetTrigger(HurtTrigger);
        HealthChange?.Invoke(_health);
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
        if (_health <= 0)
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
                _currentPlayer.TakeDamage(_damage);

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
