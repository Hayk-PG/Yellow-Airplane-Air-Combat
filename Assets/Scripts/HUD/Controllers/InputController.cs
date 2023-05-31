using System;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public enum InputType { PressShootButton, MoveJoystick}

    [Header("Joystick")]
    [SerializeField] private FixedJoystick _fixedJoystick;

    [Header("Shoot Button")]
    [SerializeField] private Btn _shootButton;

    private object[] _data = new object[1];

    private bool IsJoystickReleased
    {
        get => Mathf.Abs(_fixedJoystick.Direction.x) < 0.1f && Mathf.Abs(_fixedJoystick.Direction.y) < 0.1f;
    }

    public event Action<InputType, object[]> OnInputController;




    private void OnEnable()
    {
        _shootButton.OnSelect += OnShootButtonSelect;
    }

    private void Update()
    {
        OnJoystickMove();
    }

    private void OnShootButtonSelect()
    {
        OnInputController?.Invoke(InputType.PressShootButton, null);
    }

    private void OnJoystickMove()
    {
        if (IsJoystickReleased)
        {
            return;
        }

        _data[0] = _fixedJoystick.Direction;

        OnInputController?.Invoke(InputType.MoveJoystick, _data);
    }
}
