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
    private static Dictionary<InputType, Action> _onEnterInputDic = default;
    private static Dictionary<InputType, Action> _onStayInputDic = default;
    private static Dictionary<InputType, Action> _onExitInputDic = default;
    public static Vector2 InputVector { get; private set; }
    private void Awake()
    {
        StartSet();
    }
    private void Update()
    {
        if (Input.GetButton("Horizontal"))
        {
            var h = Input.GetAxisRaw("Horizontal");
            InputVector = Vector2.right * h;
            _onEnterInputDic[InputType.Move]?.Invoke();
        }
        if (Input.GetButton("Jump"))
        {
            _onEnterInputDic[InputType.Jump]?.Invoke();
        }
        if (Input.GetButtonUp("Jump"))
        {
            _onExitInputDic[InputType.Jump]?.Invoke();
        }
    }
    private void StartSet()
    {
        _onEnterInputDic = new Dictionary<InputType, Action>();
        _onStayInputDic = new Dictionary<InputType, Action>();
        _onExitInputDic = new Dictionary<InputType, Action>();
        for (int i = 0; i < Enum.GetValues(typeof(InputType)).Length; i++)
        {
            _onEnterInputDic.Add((InputType)i, () => { });
            _onStayInputDic.Add((InputType)i, () => { });
            _onExitInputDic.Add((InputType)i, () => { });
        }
    }
    public static void SetEnterInput(InputType type, Action action)
    {
        _onEnterInputDic[type] += action;
    }
    public static void SetStayInput(InputType type, Action action)
    {
        _onStayInputDic[type] += action; 
    }
    public static void SetExitInput(InputType type, Action action)
    {
        _onExitInputDic[type] += action; 
    }
    public bool GetInput(InputType input)
    {
        return false;
    }
}
