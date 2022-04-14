using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �L�����N�^�[�̕���
/// </summary>
public enum CharaDirection
{
    Left,
    Right,
}
/// <summary>
/// �ړ�����p�N���X
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class MoveController : MonoBehaviour
{
    /// <summary> �d�͒l </summary>
    public const float GRAVITY_POWER = -9.8f;

    #region SerializeField
    /// <summary> ���ړ����x </summary>
    [SerializeField]
    protected float _moveSpeed = 15f;
    /// <summary> �_�b�V���ړ����x </summary>
    [SerializeField]
    protected float _dashSpeed = 30f;
    /// <summary> �W�����v���x </summary>
    [SerializeField]
    protected float _jumpSpeed = 30f;
    /// <summary> �d�͒l </summary>
    [SerializeField]
    protected float _gravityScale = 1f;
    /// <summary> �ړ������l </summary>
    [SerializeField]
    private float _moveDecelerate = 0.5f;
    /// <summary> �W�����v�����l </summary>
    [SerializeField]
    private float _jumpDecelerate = 0.92f;
    /// <summary> �󒆈ړ������l </summary>
    [SerializeField]
    private float _flyDecelerate = 0.98f;
    /// <summary> �ڒn���� </summary>
    [SerializeField]
    private WallChecker _groundChecker = default;
    #endregion

    private Rigidbody2D _rigidbody = default;
    /// <summary> ���݂̈ړ����x </summary>
    private Vector2 _currentVelocity = default;

    private float _jumpTimer = default;
    private bool _isJumping = false;
    private bool _isStoping = false;
    private CharaDirection _direction;

    public CharaDirection CurrentDirection 
    {
        get => _direction; 
        private set
        {
            if (value != _direction)
            {
                _direction = value;
                OnChangeDirection?.Invoke();
            }
        }
    }
    public event Action OnChangeDirection = default;
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        MoveControl();
    }
    /// <summary>
    /// �d�͌v�Z
    /// </summary>
    private void AddGravity()
    {
        if (_isJumping) 
        { 
            _jumpTimer -= Time.deltaTime;
            if (_jumpTimer < 0)
            {
                _isJumping = false;
            }
            _currentVelocity.y *= _jumpDecelerate;
            return; 
        }
        _currentVelocity.y = _gravityScale * GRAVITY_POWER;
    }
    /// <summary>
    /// �ړ����x����
    /// </summary>
    private void MoveControl()
    {
        if (_isStoping)
        {
            _rigidbody.velocity = Vector2.zero;
            return;
        }
        AddGravity();
        _rigidbody.velocity = _currentVelocity;
        if (!_groundChecker.IsWalled())
        {
            _currentVelocity.x *= _flyDecelerate;
        }
        else
        {
            _currentVelocity.x *= _moveDecelerate;
        }
    }
    /// <summary>
    /// �w������ւ̈ړ����s��
    /// </summary>
    /// <param name="dir"></param>
    public void Move(Vector2 dir)
    {
        if (!_groundChecker.IsWalled())
        {
            return;
        }
        _currentVelocity.x = _moveSpeed * dir.x;
        if (dir.x > 0)
        {
            CurrentDirection = CharaDirection.Right;
        }
        else if (dir.x < 0)
        {
            CurrentDirection = CharaDirection.Left;
        }
    }
    /// <summary>
    /// �_�b�V������
    /// </summary>
    /// <param name="dir"></param>
    public void DashMove(float dir)
    {
        _currentVelocity.x = _dashSpeed * dir;
    }
    /// <summary>
    /// �W�����v����
    /// </summary>
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
        _currentVelocity.y = _jumpSpeed;
    }
    /// <summary>
    /// �W�����v�؋󎞊Ԃ�ݒ肷��
    /// </summary>
    /// <param name="time"></param>
    public void SetJumpTime(float time)
    {
        _jumpTimer = time;
    }
    /// <summary>
    /// �w������փW�����v����
    /// </summary>
    /// <param name="dir"></param>
    public void Jump(Vector2 dir)
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
        _currentVelocity = dir.normalized * _jumpSpeed;
    }
    /// <summary>
    /// �ړ�������~����
    /// </summary>
    public void StopControl()
    {
        _isStoping = true;
    }
    /// <summary>
    /// �ړ�������J�n����
    /// </summary>
    public void StartControl()
    {
        _isStoping = false;
    }
    public void SetStartDir(CharaDirection direction)
    {
        CurrentDirection = direction;
    }
}
