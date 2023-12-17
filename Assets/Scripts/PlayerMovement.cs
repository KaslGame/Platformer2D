using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(GroundCheck))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;

    public event UnityAction<bool> Walking;
    public event UnityAction Jumping;

    private bool _onGround = false;
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    private GroundCheck _groundCheck;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _groundCheck = GetComponent<GroundCheck>();
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
        Move();
        Jump();
    }

    private void Move()
    {
        float direction = Input.GetAxis("Horizontal");
        _rigidbody2D.velocity = new Vector2(direction * _speed, _rigidbody2D.velocity.y);
        _spriteRenderer.flipX = direction < 0 ? true : false;

        if (Mathf.Abs(direction) > 0)
            Walking?.Invoke(true);
        else
            Walking?.Invoke(false);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _onGround == true)
        {
            _rigidbody2D.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            Jumping?.Invoke();
        }
    }
}
