using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateType
{
    Idle,
    GroundMove,
    Jump,
    Fall,
    WallShaving,
    Landing,
    Damage,
    Down,
    Action1,
}
public partial class StateController : MonoBehaviour
{
    /// <summary> 接地判定 </summary>
    [SerializeField]
    protected WallChecker _groundChecker = default;
    /// <summary> 壁判定 </summary>
    [SerializeField]
    protected WallChecker _wallChecker = default;
    /// <summary> 天井判定 </summary>
    [SerializeField]
    protected WallChecker _topChecker = default;

    [SerializeField]
    private MoveController _moveController = default;

    private IStateBase _currentState = default;
    private StateType _currrentStateType = default;
    private float _stateTimer = default;
    private bool _isChangeState = default;

    public Vector2 InputVector = default;

    /// <summary> キャラクター向き </summary>
    protected CharaDirection _direction;
    /// <summary> 向き変更時イベント </summary>
    public event Action OnChangeDirection = default;
    public CharaDirection CurrentDirection
    {
        get => _direction;
        protected set
        {
            if (value != _direction)
            {
                _direction = value;
                OnChangeDirection?.Invoke();
            }
        }
    }
    /// <summary>
    /// ステートマシーン用インターフェース
    /// </summary>
    private interface IStateBase
    {
        /// <summary>
        /// ステート開始時
        /// </summary>
        /// <param name="controller"></param>
        void OnEnter(StateController controller);
        /// <summary>
        /// ステートUpdate・判定
        /// </summary>
        /// <param name="controller"></param>
        void OnUpdate(StateController controller);
        /// <summary>
        /// ステート移動関係・判定
        /// </summary>
        /// <param name="controller"></param>
        void OnFixedUpdate(StateController controller);
        /// <summary>
        /// ステート終了時
        /// </summary>
        /// <param name="controller"></param>
        void OnLeave(StateController controller);
    }
    private void Start()
    {

    }
    private void Update()
    {
        _currentState.OnUpdate(this);
    }
    private void FixedUpdate()
    {
        _currentState.OnFixedUpdate(this);
    }
    private IStateBase GetState(StateType stateType)
    {
        switch (stateType)
        {
            case StateType.Idle:
                break;
            case StateType.GroundMove:
                break;
            case StateType.Jump:
                break;
            case StateType.Fall:
                break;
            case StateType.WallShaving:
                break;
            case StateType.Landing:
                break;
            case StateType.Damage:
                break;
            case StateType.Down:
                break;
            case StateType.Action1:
                break;
            default:
                break;
        }
        return _currentState;
    }
    private bool IsGround()
    {
        return _groundChecker.IsWalled();
    }
    private bool IsFrontWalled()
    {
        return _wallChecker.IsWalled(_direction);
    }
    private bool IsTopWalled()
    {
        return _topChecker.IsWalled();
    }
    public void ChangeState(StateType nextState)
    {
        _currentState.OnLeave(this);
        _currentState = GetState(nextState);
        _currentState.OnEnter(this);
    }
    public void Jump()
    {
        if (_currrentStateType == StateType.WallShaving)
        {
            _moveController.WallJump();
            ChangeState(StateType.Jump);
            return;
        }
        if (_groundChecker.IsWalled())
        {
            _moveController.StartJump();
            ChangeState(StateType.Jump);
        }
    }
}
