using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterDirection
{
    Left,
    Right,
}
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
    private float _moveDecelerate = 0.98f;
    [SerializeField]
    private float _jumpDecelerate = 0.98f;
    [SerializeField]
    private float _flyDecelerate = 0.98f;

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
        MoveControl();
    }
    private void AddGravity()
    {
        if (_isJumping) 
        { 
            _jumpTimer -= Time.deltaTime;
            if (_jumpTimer < 0)
            {
                _isJumping = false;
            }
            _moveVector.y *= _jumpDecelerate;
            return; 
        }
        _moveVector.y = _gravityScale * GRAVITY_POWER;
    }
    private void MoveControl()
    {
        AddGravity();
        _rigidbody.velocity = _moveVector;
        if (!_groundChecker.IsWalled())
        {
            _moveVector.x *= _flyDecelerate;
        }
        else
        {
            _moveVector.x *= _moveDecelerate;
        }
    }
    public void Move(Vector2 dir)
    {
        if (!_groundChecker.IsWalled())
        {
            return;
        }
        _moveVector.x = _moveSpeed * dir.x;
    }
    public void DashMove(float dir)
    {
        _moveVector.x = _runSpeed * dir;
    }
    public void StartJump()
    {
        if (!_groundChecker.IsWalled())
        {
            return;
        }
        if (_isJumping)
        {
            return;
        }
        _isJumping = true;
        _moveVector.y = _jumpSpeed;
    }
    public void SetJumpTime(float time)
    {
        _jumpTimer = time;
    }
    public void Jump(Vector2 dir)
    {
        if (!_groundChecker.IsWalled())
        {
            return;
        }
        if (!_isJumping)
        {
            _isJumping = true;
        }
        _moveVector = dir.normalized * _jumpSpeed;
    }
}
