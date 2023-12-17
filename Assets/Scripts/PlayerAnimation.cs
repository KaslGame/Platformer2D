using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(GroundCheck))]
public class PlayerAnimation : MonoBehaviour
{
    const string WalkingAnimation = "Walking";
    const string OnGroundBool = "OnGround";
    const string JumpingAnimation = "Jumping";

    private GroundCheck _groundCheck;
    private PlayerMovement _playerMovement;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _groundCheck = GetComponent<GroundCheck>();
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void OnEnable()
    {
        _playerMovement.Walking += OnWalking;
        _playerMovement.Jumping += OnJumping;
        _groundCheck.StateOnGroundChange += OnStateOnGroundChange;
    }

    private void OnDisable()
    {
        _playerMovement.Walking -= OnWalking;
        _playerMovement.Jumping -= OnJumping;
    }

    private void OnWalking(bool isWalk)
    {
        _animator.SetBool(WalkingAnimation, isWalk);
    }

    private void OnJumping()
    {
        _animator.SetTrigger(JumpingAnimation);
    }

    private void OnStateOnGroundChange(bool onGround)
    {
        _animator.SetBool(OnGroundBool, onGround);
    }
}
