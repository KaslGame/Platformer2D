using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(GroundCheck))]
[RequireComponent(typeof(PlayerMovement))]
public class PlayerCombat : MonoBehaviour
{
    private readonly int AttackTrigger = Animator.StringToHash("Attack");

    private Animator _animator;
    private GroundCheck _groundCheck;
    private PlayerMovement _playerMovement;

    private bool _onGround;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _groundCheck = GetComponent<GroundCheck>();
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void OnEnable()
    {
        _groundCheck.StateOnGroundChange += OnStateOnGroundChange;
    }

    private void OnDisable()
    {
        _groundCheck.StateOnGroundChange -= OnStateOnGroundChange;
    }

    private void OnStateOnGroundChange(bool onGround)
    {
        _onGround = onGround;
    }

    private void Update()
    {
        if (_onGround == false)
            return;

        TryAttackAnimation();
    }
    private void TryAttackAnimation()
    {
        if (Input.GetMouseButtonDown(0) && _playerMovement.IsStop == false)
            _animator.SetTrigger(AttackTrigger);
    }
}
