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
    [SerializeField]
    private WallChecker _groundChecker = default;

    private Rigidbody2D _rigidbody = default;
    private Vector2 _moveVector = default;

    private float _jumpTimer = default;

    private bool _isJumping = false;
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
        if (_isJumping) 
        { 
            _jumpTimer -= Time.deltaTime;
            if (_jumpTimer <= 0)
            {
                _isJumping = false;
            }
            return; 
        }
        _moveVector.y = _gravityScale * GRAVITY_POWER;
    }
    public void Move(Vector2 dir)
    {
        if (!_groundChecker.IsWalled())
        {
            return;
        }
        _moveVector.x = _moveSpeed * dir.x;
    }
    public void Jump(float time)
    {
        if (!_groundChecker.IsWalled())
        {
            return;
        }
        if (!_isJumping)
        {
            _isJumping = true;
            _jumpTimer = time;
        }
        _moveVector.y = _jumpSpeed;
    }
}
