using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// キャラクターの方向
/// </summary>
public enum CharaDirection
{
    Left = -1,
    Right = 1,
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
    /// <summary> 壁ジャンプ速度 </summary>
    [SerializeField]
    protected float _wallJumpSpeed = 15f;
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
    /// <summary> 壁ジャンプの方向 </summary>
    [SerializeField]
    private Vector2 _wallJumpDir = Vector2.one;
    #endregion

    private Rigidbody2D _rigidbody = default;
    /// <summary> 現在の移動速度 </summary>
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
    /// 重力計算
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
    /// 移動速度制御
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
    /// 指定方向への移動を行う
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
        _currentVelocity.y = _jumpSpeed;
    }

    /// <summary>
    /// 壁ジャンプする
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
