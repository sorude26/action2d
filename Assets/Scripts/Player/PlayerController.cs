using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// PlayerëÄçÏópÉNÉâÉX
/// </summary>
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private MoveController _moveController = default;
    [SerializeField]
    GameObject _characterBody = default;
    [SerializeField]
    private float _jumpTime = 1f;
    [SerializeField]
    private float _jumpStartTime = 0.02f;
    private float _jumpTimer = 0f;
    private bool _isPlaying = false;
    private void Start()
    {
        PlayerInputManager.SetEnterInput(InputType.Move,()=>MoveInput());
        PlayerInputManager.SetEnterInput(InputType.Jump,()=>JumpInput());
        PlayerInputManager.SetExitInput(InputType.Jump,()=>JumpExit());
        _moveController.OnChangeDirection += () =>
        {
            if (_moveController.CurrentDirection == CharaDirection.Left)
            {
                _characterBody.transform.localScale = Vector3.one;
            }
            else if (_moveController.CurrentDirection == CharaDirection.Right)
            {
                _characterBody.transform.localScale = new Vector3(-1, 1, 1);
            }
        };
    }
    private void MoveInput()
    {
        if (_moveController != null)
        {
            _moveController.Move(PlayerInputManager.InputVector);
        }
    }
    private void JumpInput()
    {
        if (_moveController != null)
        {
            if (_isPlaying)
            {
                if (_jumpTimer < _jumpTime)
                {
                    _jumpTimer += Time.deltaTime;
                    _moveController.SetJumpTime(_jumpTimer);
                }
            }
            else
            {
                _isPlaying = true;
                _jumpTimer = _jumpStartTime;
                _moveController.SetJumpTime(_jumpTimer);
                _moveController.StartJump();
            }
        }
    }
    private void JumpExit()
    {
        _jumpTimer = 0;
        _isPlaying = false;
    }
}
