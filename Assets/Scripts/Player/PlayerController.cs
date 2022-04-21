using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

/// <summary>
/// PlayerëÄçÏópÉNÉâÉX
/// </summary>
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private MoveController _moveController = default;
    [SerializeField]
    private StateController _stateController = default;
    [SerializeField]
    GameObject _characterBody = default;
    [SerializeField]
    private float _jumpTime = 1f;
    [SerializeField]
    private float _jumpStartTime = 0.02f;
    [SerializeField]
    private CharaDirection _startDir = CharaDirection.Right;
    private float _jumpTimer = 0f;
    private bool _isJump = false;
    private void Start()
    {
        PlayerInputManager.SetEnterInput(InputType.Move, () => InputMove());
        PlayerInputManager.SetEnterInput(InputType.Jump, () => InputJump());
        PlayerInputManager.SetExitInput(InputType.Move, () => InputMove());
        PlayerInputManager.SetExitInput(InputType.Jump, () => ExitJump());
        _moveController.OnChangeDirection += () =>
        {
            if (_moveController.CurrentDirection == CharaDirection.Right)
            {
                _characterBody.transform.localScale = Vector3.one;
            }
            else if (_moveController.CurrentDirection == CharaDirection.Left)
            {
                _characterBody.transform.localScale = new Vector3(-1, 1, 1);
            }
        };
        _moveController.SetStartDir(_startDir);
        _stateController.OnChangeState += () => ChangeState();
    }
    private void InputMove()
    {
        _stateController.InputVector = PlayerInputManager.InputVector;
    }
    private void InputJump()
    {
        if (_stateController.CurrentState == StateType.Jump)
        {
            if (_jumpTimer > _jumpTime)
            {
                return;
            }
            _jumpTimer += Time.deltaTime;
            _stateController.SetTimer(_jumpStartTime);
        }
        else if (!_isJump)
        {
            _isJump = true;
            _jumpTimer = 0;
            _stateController.SetTimer(_jumpStartTime);
            _stateController.Jump();
        }
    }
    private void ExitJump()
    {
        _isJump = false;
    }
    private void ChangeState()
    {
        _jumpTimer = 0;
    }
}
