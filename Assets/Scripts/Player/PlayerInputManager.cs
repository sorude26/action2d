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
    private Dictionary<InputType, Action> _onEnterInputDic = default;
    private Dictionary<InputType,Action> _onStayInputDic = default;
    private Dictionary<InputType,Action> _onExitInputDic = default;
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
    private void StartSet()
    {
        _onEnterInputDic = new Dictionary<InputType, Action>();
        _onStayInputDic = new Dictionary<InputType, Action>();
        _onExitInputDic = new Dictionary<InputType, Action>();
        /*
        for (int i = 0; i < System.Enum.GetValues(typeof(InputType)).Length; i++)
        {
            _onEnterInputDic.Add((InputType)i, () => { });
        }
        */
    }
    public void SetEnterInput(InputType type,Action action)
    {
        _onEnterInputDic.Add(type, action);
    }
    public void SetStayInput(InputType type,Action action)
    {
        _onStayInputDic.Add(type, action);
    }
    public void SetExitInput(InputType type,Action action)
    {
        _onExitInputDic.Add(type, action);
    }
    public bool GetInput(InputType input)
    {
        return false;
    }
}
