using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �L�����N�^�[�̕���
/// </summary>
public enum CharaDirection
{
    Left = -1,
    Right = 1,
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
    /// <summary> �ǃW�����v���x </summary>
    [SerializeField]
    protected float _wallJumpSpeed = 15f;
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
    /// <summary> �ǃW�����v�̕��� </summary>
    [SerializeField]
    private Vector2 _wallJumpDir = Vector2.one;
    #endregion

    private Rigidbody2D _rigidbody = default;
    /// <summary> ���݂̈ړ����x </summary>
    private Vector2 _currentVelocity = default;

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

    /// <summary>
    /// �d�͌v�Z
    /// </summary>
    public void AddGravity()
    {
        _currentVelocity.y = _gravityScale * GRAVITY_POWER;
    }
    public void AddGravityForJump()
    {
        _currentVelocity.y *= _jumpDecelerate;
    }
    /// <summary>
    /// �ړ����x����
    /// </summary>
    public void MoveControl()
    {
        if (_isStoping)
        {
            _rigidbody.velocity = Vector2.zero;
            return;
        }
        _rigidbody.velocity = _currentVelocity;
    }
    public void MoveDecelerate()
    {
        _currentVelocity.x *= _moveDecelerate;
    }
    public void FlyDecelerate()
    {
        _currentVelocity.x *= _flyDecelerate;
    }
    public void StopMove()
    {
        _currentVelocity.x = 0;
    }
    /// <summary>
    /// �w������ւ̈ړ����s��
    /// </summary>
    /// <param name="dir"></param>
    public void Move(Vector2 dir)
    {
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
        _currentVelocity.y = _jumpSpeed;
    }

    /// <summary>
    /// �ǃW�����v����
    /// </summary>
    /// <param name="dir"></param>
    public void WallJump()
    {
        if (CurrentDirection == CharaDirection.Left)
        {
            CurrentDirection = CharaDirection.Right;
        }
        else
        {
            CurrentDirection = CharaDirection.Left;
        }
        Vector2 jumpDir = _wallJumpDir;
        jumpDir.x *= (int)CurrentDirection;
        _currentVelocity = jumpDir.normalized * _wallJumpSpeed;
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
