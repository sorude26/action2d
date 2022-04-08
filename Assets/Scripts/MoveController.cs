using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MoveController : MonoBehaviour
{
    public const float GRAVITY_POWER = -9.8f;

    [SerializeField]
    protected float _maxFallSpeed = 20f;
    [SerializeField]
    protected float _moveSpeed = 2f;
    [SerializeField]
    protected float _runSpeed = 6f;
    [SerializeField]
    protected float _jumpSpeed = 10f;
    [SerializeField]
    protected float _gravityScale = 1f;

    private Rigidbody2D _rigidbody = default;
    private Vector2 _moveVector = default;

    private float _jumpVector = default;

    private bool _isJumping = false;
    private bool _isGrounded = false;
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        AddGravity();
        _rigidbody.velocity = _moveVector;
    }
    private void AddGravity()
    {
        _moveVector.y = _gravityScale * GRAVITY_POWER;
    }
    public void Move(Vector2 dir)
    {
        _moveVector.x = _moveSpeed * dir.x;
    }
    public void Jump()
    {
        if (!_isJumping)
        {
            _isJumping = true;
        }
        _moveVector.y = _jumpSpeed;
    }
}
