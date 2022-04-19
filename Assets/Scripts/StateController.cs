using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateType
{
    Idle,
    GroundMove,
    Jump,
    Fly,
    Landing,
    Damage,
    Down,
    Action1,
}
public partial class StateController : MonoBehaviour
{
    /// <summary> �ڒn���� </summary>
    [SerializeField]
    protected WallChecker _groundChecker = default;
    /// <summary> �ǔ��� </summary>
    [SerializeField]
    protected WallChecker _wallChecker = default;
    /// <summary> �V�䔻�� </summary>
    [SerializeField]
    protected WallChecker _topChecker = default;

    [SerializeField]
    private MoveController _moveController = default;

    private IStateBase _currentState = default;
    private float _stateTimer = default;
    private bool _isChangeState = default;
    /// <summary> �L�����N�^�[���� </summary>
    protected CharaDirection _direction;
    /// <summary> �����ύX���C�x���g </summary>
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
    private interface IStateBase
    {
        void OnEnter(StateController controller);
        void OnUpdate(StateController controller);
        void OnFixedUpdate(StateController controller);
        IEnumerator OnLeave(StateController controller);
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
        return _currentState;
    }
    private IEnumerator ChangeState(StateType nextState)
    {
        yield return _currentState.OnLeave(this);
        _currentState = GetState(nextState);
        _currentState.OnEnter(this);
    }
    public void StartChangeState(StateType nextState)
    {
        StartCoroutine(ChangeState(nextState));
    }
}
