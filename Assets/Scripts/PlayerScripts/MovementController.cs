using UnityEngine;

public class MovementController : BaseAirplaneMovementManager
{
    private void OnEnable()
    {
        Reference.Manager.InputController.OnInputController += OnInputController;
    }

    private void FixedUpdate() 
    {
        Move();
    }

    private void OnInputController(InputController.InputType inputType, object[] data)
    {
        Rotate(inputType, data);
        HandleJoystickRelease(inputType);
    }

    private void Move() 
    {
        Vector2 velocity = Velocity(transform.right, _speed);
        Move(velocity);
    }

    private void Rotate(InputController.InputType inputType, object[] data) 
    {
        if (inputType != InputController.InputType.MoveJoystick)
        {
            return;
        }

        Vector2 joystickInput = (Vector2)data[0];
        float angle = Angle(joystickInput);
        Rotate(angle);
        UpdateRotationDirection();
    }

    private void HandleJoystickRelease(InputController.InputType inputType)
    {
        if(inputType != InputController.InputType.ReleaseJoystick)
        {
            return;
        }

        SetAnimationState(AnimationState.Idle);
        SetSpeed(_defaultSpeed, 3f, _propellerDefaultRate, 1f);
    }
}