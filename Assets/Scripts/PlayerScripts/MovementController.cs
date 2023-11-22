using UnityEngine;

public class MovementController : BaseAirplaneMovementManager
{
    private object[] _movementData = new object[1];




    private void OnEnable()
    {
        GameEventHandler.OnEvent += OnGameEvent;
    }

    private void OnDisable()
    {
        GameEventHandler.OnEvent -= OnGameEvent;
    }

    private void FixedUpdate() 
    {
        Move();
        PlayerMoveBroadcast();
    }

    private void OnGameEvent(GameEventType gameEventType, object[] data)
    {
        HandleJoystickMovement(gameEventType, data);
        HandleJoystickRelease(gameEventType);
        OnLastHopeDefenderMessageActivity(gameEventType, data);
        HandlePauseButtonClick(gameEventType, data);
    }

    private void HandleJoystickMovement(GameEventType gameEventType, object[] data)
    {
        if (gameEventType != GameEventType.OnJoystickMove)
        {
            return;
        }

        Rotate(direction: (Vector2)data[0]);
    }

    private void HandleJoystickRelease(GameEventType gameEventType)
    {
        if (gameEventType != GameEventType.OnJoystickRelease)
        {
            return;
        }

        HandleJoystickRelease();
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

    private void Rotate(Vector2 direction) 
    {
        float angle = Angle(direction);
        Rotate(angle);
        UpdateRotationDirection();
    }

    private void HandleJoystickRelease()
    {
        SetAnimationState(AnimationState.Idle);
        SetSpeed(_defaultSpeed, 3f, _propellerDefaultRate, 1f);
    }
}