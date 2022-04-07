using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private MoveController _moveController = default;
    private void Start()
    {
        PlayerInputManager.OnStayInput += MoveInput;
    }
    private void MoveInput(InputType input)
    {
        if(input != InputType.Move) { return; }
        if (_moveController != null)
        {
            _moveController.Move(PlayerInputManager.InputVector);
        }
    }
}
