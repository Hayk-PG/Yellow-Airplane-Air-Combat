using UnityEngine;

public class MovementController : BaseAirplaneMovementManager
{
    private object[] _movementData = new object[1];




    private void OnEnable()
    {
        GameEventHandler.OnEvent += OnGameEvent;
        Reference.Manager.InputController.OnInputController += OnInputController;
    }

    private void OnDisable()
    {
        GameEventHandler.OnEvent -= OnGameEvent;
        Reference.Manager.InputController.OnInputController -= OnInputController;
    }

    private void FixedUpdate() 
    {
        Move();
        PlayerMoveBroadcast();
    }

    private void OnGameEvent(GameEventType gameEventType, object[] data)
    {
        OnLastHopeDefenderMessageActivity(gameEventType, data);
        HandlePauseButtonClick(gameEventType, data);
    }

    private void OnLastHopeDefenderMessageActivity(GameEventType gameEventType, object[] data)
    {
        if (gameEventType != GameEventType.OnLastHopeDefenderMessageActivity)
        {
            return;
        }

        bool? isLastHopeDefenderMessageActive = (bool?)data[0];
        _externalSoundSource.Volume = isLastHopeDefenderMessageActive.HasValue ? isLastHopeDefenderMessageActive.Value ? 0f : 1f : 0.25f;
    }

    private void HandlePauseButtonClick(GameEventType gameEventType, object[] data)
    {
        if (gameEventType != GameEventType.OnPauseButtonClick)
        {
            return;
        }

        _externalSoundSource.Volume = (bool)data[0] ? 0f : 1f;
    }

    /// <summary>
    /// Event handler for the InputController's input events.
    /// </summary>
    /// <param name="inputType">The type of input.</param>
    /// <param name="data">Additional data associated with the input.</param>
    private void OnInputController(InputController.InputType inputType, object[] data)
    {
        Rotate(inputType, data);
        HandleJoystickRelease(inputType);
    }

    /// <summary>
    /// Moves the airplane in the current direction.
    /// </summary>
    private void Move() 
    {      
        Vector2 velocity = Velocity(transform.right, _speed);
        Move(velocity);
    }

    private void PlayerMoveBroadcast()
    {
        _movementData[0] = _rigidbody.position;
        GameEventHandler.RaiseEvent(GameEventType.PlayerMoveBroadcast, _movementData);
    }

    /// <summary>
    /// Rotates the airplane based on joystick input.
    /// </summary>
    /// <param name="inputType">The type of input.</param>
    /// <param name="data">Additional data associated with the input.</param>
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

    /// <summary>
    /// Handles the release of the joystick input.
    /// </summary>
    /// <param name="inputType">The type of input.</param>
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