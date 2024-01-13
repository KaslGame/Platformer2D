using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(GroundCheck))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Health))]
public class PlayerAnimation : MonoBehaviour
{
    private readonly int HurtTrigger = Animator.StringToHash("Hurt");
    private readonly int JumpTrigger = Animator.StringToHash("Jumping");
    const string WalkingAnimation = "Walking";
    const string OnGroundBool = "OnGround";

    private GroundCheck _groundCheck;
    private PlayerMovement _playerMovement;
    private Animator _animator;
    private Health _playerHealth;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _groundCheck = GetComponent<GroundCheck>();
        _playerMovement = GetComponent<PlayerMovement>();
        _playerHealth = GetComponent<Health>();
    }

    private void OnEnable()
    {
        _playerMovement.Jumping += OnJumping;
        _groundCheck.StateOnGroundChange += OnStateOnGroundChange;
        _playerHealth.HealthChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        _playerMovement.Jumping -= OnJumping;
        _groundCheck.StateOnGroundChange -= OnStateOnGroundChange;
        _playerHealth.HealthChanged += OnHealthChanged;
    }

    private void Update()
    {
        Walking();
    }

    private void OnHealthChanged(int health, int oldHealth)
    {
        if (oldHealth > health)
            _animator.SetTrigger(HurtTrigger);
    }

    private void Walking()
    {
        if (_playerMovement.IsStop)
        {
            _animator.SetBool(WalkingAnimation, false);
            return;
        }

        float direction = Input.GetAxis("Horizontal");

        if (Mathf.Abs(direction) > 0)
            _animator.SetBool(WalkingAnimation, true);
        else
            _animator.SetBool(WalkingAnimation, false);
    }

    private void OnJumping()
    {
        _animator.SetTrigger(JumpTrigger);
    }

    private void OnStateOnGroundChange(bool onGround)
    {
        _animator.SetBool(OnGroundBool, onGround);
    }
}
