using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(GroundCheck))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Player))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;

    public event UnityAction Jumping;

    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    private GroundCheck _groundCheck;

    private bool _onGround = false;
    private bool _isStop = false;

    public bool IsStop => _isStop;

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
        if (_isStop)
        {
            _rigidbody2D.velocity = new Vector2(Vector2.zero.x, Vector2.zero.y);
            return;
        }

        Move();
        Jump();
    }

    private void ChangeStopStatus()
    {
        if (_isStop) 
            _isStop = false;
        else
            _isStop = true;
    }

    private void Move()
    {
        float direction = Input.GetAxis("Horizontal");
        _rigidbody2D.velocity = new Vector2(direction * _speed, _rigidbody2D.velocity.y);
        TryFlip(direction);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _onGround == true)
        {
            _rigidbody2D.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            Jumping?.Invoke();
        }
    }

    private void TryFlip(float direction)
    {
        if (direction > 0)
            transform.rotation = Quaternion.identity;
        else if (direction < 0)
            transform.rotation = Quaternion.Euler(0, 180, 0);
    }
}
