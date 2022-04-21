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

    //――――――STATE――――――
    private StateIdle _sIdle = new StateIdle();
    private StateJump _sJump = new StateJump();
    private StateFall _sFall = new StateFall();
    private StateLanding _sLanding = new StateLanding();
    private StateGroundMove _sGroundMove = new StateGroundMove();
    private StateWallShaving _sWallShaving = new StateWallShaving();
    //-――――――――――――――
    public Vector2 InputVector = default;
    public event Action OnChangeState = default;
    /// <summary> 向き変更時イベント </summary>
    public event Action OnChangeDirection = default;
    public StateType CurrentState { get => _currrentStateType; }

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
        _currentState = _sIdle;
    }
    private void Update()
    {
        _currentState.OnUpdate(this);
    }
    private void FixedUpdate()
    {
        _currentState.OnFixedUpdate(this);
    }
    private bool IsGround()
    {
        return _groundChecker.IsWalled();
    }
    private bool IsFrontWalled()
    {
        return _wallChecker.IsWalled(_moveController.CurrentDirection);
    }
    private bool IsTopWalled()
    {
        return _topChecker.IsWalled();
    }
    public void ChangeState(StateType nextState)
    {
        _currentState.OnLeave(this);
        switch (nextState)
        {
            case StateType.Idle:
                _currentState = _sIdle;
                break;
            case StateType.GroundMove:
                _currentState = _sGroundMove;
                break;
            case StateType.Jump:
                _currentState = _sJump;
                break;
            case StateType.Fall:
                _currentState = _sFall;
                break;
            case StateType.WallShaving:
                _currentState = _sWallShaving;
                break;
            case StateType.Landing:
                _currentState = _sLanding;
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
        OnChangeState?.Invoke();
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
        if (_currrentStateType == StateType.Idle || _currrentStateType == StateType.GroundMove)
        {
            _moveController.StartJump();
            ChangeState(StateType.Jump);
            return;
        }
    }
    public void SetTimer(float time)
    {
        _stateTimer = time;
    }
}
