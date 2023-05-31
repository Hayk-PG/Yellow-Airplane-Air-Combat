using System;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public enum InputType { PressShootButton, MoveJoystick}

    [Header("Joystick")]
    [SerializeField] private FixedJoystick _fixedJoystick; // Reference to the FixedJoystick component for movement input

    [Header("Shoot Button")]
    [SerializeField] private Btn _shootButton; // Reference to the Btn component for shoot button input

    private object[] _data = new object[2]; // Array to hold input data

    private bool IsJoystickReleased
    {
        get => Mathf.Abs(_fixedJoystick.Direction.x) < 0.1f && Mathf.Abs(_fixedJoystick.Direction.y) < 0.1f;
    }

    public event Action<InputType, object[]> OnInputController; // Event triggered when input is received




    private void OnEnable()
    {
        _shootButton.OnPointerDownHandler += ()=> OnShootButtonHold(true);
        _shootButton.OnPointerUpHandler += () => OnShootButtonHold(false);
    }

    private void Update()
    {
        OnJoystickMove();
    }

    // Handles the joystick movement input.
    private void OnJoystickMove()
    {
        if (IsJoystickReleased)
        {
            return;
        }

        _data[0] = _fixedJoystick.Direction;

        OnInputController?.Invoke(InputType.MoveJoystick, _data);
    }

    // Handles the shoot button input.
    private void OnShootButtonHold(bool isPointerDown)
    {
        _data[1] = isPointerDown;

        OnInputController?.Invoke(InputType.PressShootButton, _data);
    }
}