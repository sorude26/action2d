using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// キャラクターの方向
/// </summary>
public enum CharaDirection
{
    Left,
    Right,
}
/// <summary>
/// 移動制御用クラス
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class MoveController : MonoBehaviour
{
    /// <summary> 重力値 </summary>
    public const float GRAVITY_POWER = -9.8f;

    #region SerializeField
    /// <summary> 横移動速度 </summary>
    [SerializeField]
    protected float _moveSpeed = 15f;
    /// <summary> ダッシュ移動速度 </summary>
    [SerializeField]
    protected float _dashSpeed = 30f;
    /// <summary> ジャンプ速度 </summary>
    [SerializeField]
    protected float _jumpSpeed = 30f;
    /// <summary> 重力値 </summary>
    [SerializeField]
    protected float _gravityScale = 1f;
    /// <summary> 移動減速値 </summary>
    [SerializeField]
    private float _moveDecelerate = 0.5f;
    /// <summary> ジャンプ減速値 </summary>
    [SerializeField]
    private float _jumpDecelerate = 0.92f;
    /// <summary> 空中移動減速値 </summary>
    [SerializeField]
    private float _flyDecelerate = 0.98f;
    /// <summary> 接地判定 </summary>
    [SerializeField]
    private WallChecker _groundChecker = default;
    #endregion

    private Rigidbody2D _rigidbody = default;
    /// <summary> 現在の移動速度 </summary>
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
    /// 重力計算
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
    /// 移動速度制御
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
    /// 指定方向への移動を行う
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
    /// ダッシュする
    /// </summary>
    /// <param name="dir"></param>
    public void DashMove(float dir)
    {
        _currentVelocity.x = _dashSpeed * dir;
    }
    /// <summary>
    /// ジャンプする
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
    /// ジャンプ滞空時間を設定する
    /// </summary>
    /// <param name="time"></param>
    public void SetJumpTime(float time)
    {
        _jumpTimer = time;
    }
    /// <summary>
    /// 指定方向へジャンプする
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
    /// 移動制御を停止する
    /// </summary>
    public void StopControl()
    {
        _isStoping = true;
    }
    /// <summary>
    /// 移動制御を開始する
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
