using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private MoveController _moveController = default;
    [SerializeField]
    private float _jumpTime = 1f;
    private void Start()
    {
        PlayerInputManager.OnStayInput += MoveInput;
        PlayerInputManager.OnEnterInput += JumpInput;
    }
    private void MoveInput(InputType input)
    {
        if(input != InputType.Move) { return; }
        if (_moveController != null)
        {
            _moveController.Move(PlayerInputManager.InputVector);
        }
    }
    private void JumpInput(InputType input)
    {
        if (input != InputType.Jump) { return; }
        if (_moveController != null)
        {
            _moveController.Jump(_jumpTime);
        }
    }
}
