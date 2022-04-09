using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InputType
{
    Up,
    Down,
    Left,
    Right,
    Move,
    Jump,
    Fire1,
    Fire2,
    Fire3,
    Submit,
}
public class PlayerInputManager : MonoBehaviour
{
    public static event Action<InputType> OnEnterInput = default;
    public static event Action<InputType> OnStayInput = default;
    public static event Action<InputType> OnExitInput = default;
    public static Vector2 InputVector { get; private set; }
    private void Update()
    {
        var h = Input.GetAxisRaw("Horizontal");
        InputVector = Vector2.right * h;
        OnStayInput?.Invoke(InputType.Move);
        if (Input.GetButtonDown("Jump"))
        {
            OnEnterInput?.Invoke(InputType.Jump);
        }
    }
    public bool GetInput(InputType input)
    {
        return false;
    }
}
