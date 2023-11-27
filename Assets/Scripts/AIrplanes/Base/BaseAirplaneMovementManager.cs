using System.Collections;
using UnityEngine;
using Pautik;

/// <summary>
/// Abstract base class for managing the movement of an airplane.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AirplaneAnimationManager))]
public abstract class BaseAirplaneMovementManager : MonoBehaviour
{
    protected enum AnimationState { Idle, TurnRight, TurnLeft, Dodge }

    [Header("Base Components")]
    [SerializeField] protected Rigidbody2D _rigidbody;
    [SerializeField] protected AirplaneAnimationManager _airplaneAnimationManager;
    [SerializeField] protected ExternalSoundSource _externalSoundSource;

    protected Vector2 _currentVelocity;

    protected Quaternion _previousRotation;
    protected Quaternion _currentRotation;

    [Header("Speed")]
    [SerializeField] protected float _speed;
    [SerializeField] protected float _rotationSpeed;

    protected float _defaultSpeed;
    protected float _newSpeed;
    protected float _speedChangeTime;
    protected float _propellerDefaultRate;
    protected float _propellerNewRate;
    protected float _propellerRateChangeTime;

    public Rigidbody2D Rigidbody => _rigidbody;




    protected virtual void Start()
    {
        InitializeSpeed();
        InitializePreviousRotation();
        StartCoroutine(UpdateSpeed());
    }

    /// <summary>
    /// Moves the rigidbody based on the specified velocity.
    /// </summary>
    /// <param name="velocity">The velocity vector for movement.</param>
    protected virtual void Move(Vector2 velocity)
    {
        _rigidbody.velocity = Vector2.SmoothDamp(_rigidbody.velocity, velocity, ref _currentVelocity, 0.1f, 10f, 5f);
    }

    /// <summary>
    /// Rotates the airplane to the specified angle.
    /// </summary>
    /// <param name="angle">The target rotation angle.</param>
    protected virtual void Rotate(float angle)
    {
        _rigidbody.MoveRotation(Quaternion.Slerp(_rigidbody.transform.rotation, Quaternion.Euler(0, 0, angle), _rotationSpeed * Time.fixedDeltaTime));
    }

    /// <summary>
    /// Calculates the velocity vector based on the given direction and speed.
    /// </summary>
    /// <param name="direction">The direction vector.</param>
    /// <param name="speed">The speed value.</param>
    /// <returns>The calculated velocity vector.</returns>
    protected Vector2 Velocity(Vector2 direction, float speed)
    {
        return direction * speed * Time.fixedDeltaTime;
    }

    /// <summary>
    /// Calculates the angle based on the given direction vector.
    /// </summary>
    /// <param name="direction">The direction vector.</param>
    /// <returns>The calculated angle in degrees.</returns>
    protected virtual float Angle(Vector2 direction)
    {
        return Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    }

    /// <summary>
    /// Initializes the previous rotation to the current rotation of the transform.
    /// </summary>
    protected virtual void InitializePreviousRotation()
    {
        _previousRotation = _rigidbody.transform.rotation;
    }

    /// <summary>
    /// Updates the current and previous rotation values and triggers animations based on the rotation direction.
    /// </summary>
    protected virtual void UpdateRotationDirection()
    {
        _currentRotation = _rigidbody.transform.rotation;

        float angleDifference = Converter.GetAngleDifference(_currentRotation.eulerAngles.z, _previousRotation.eulerAngles.z);

        if (angleDifference > 0f)
        {
            SetAnimationState(AnimationState.TurnLeft);
            SetSpeed(_defaultSpeed * 1.5f, 1f, 1.5f, 0.001f);
        }
        else if (angleDifference < 0f)
        {
            SetAnimationState(AnimationState.TurnRight);
            SetSpeed(_defaultSpeed * 1.5f, 1f, 1.5f, 0.001f);
        }

        _previousRotation = _currentRotation;
    }

    /// <summary>
    /// Sets the animation state of the airplane.
    /// </summary>
    /// <param name="animationState">The animation state to set.</param>

    protected virtual void SetAnimationState(AnimationState animationState)
    {
        switch (animationState)
        {
            case AnimationState.Idle: UpdateAnimationStates(true, false, false, false); break;
            case AnimationState.TurnRight: UpdateAnimationStates(false, true, false, false); break;
            case AnimationState.TurnLeft: UpdateAnimationStates(false, false, true, false); break;
            case AnimationState.Dodge: UpdateAnimationStates(false, false, false, true); break;
        }
    }

    /// <summary>
    /// Updates the animation states of the airplane.
    /// </summary>
    /// <param name="playIdleAnimation">Whether to play the idle animation.</param>
    /// <param name="playRightTurnAnimation">Whether to play the right turn animation.</param>
    /// <param name="playLeftTurnAnimation">Whether to play the left turn animation.</param>
    /// <param name="playDodgeAnimation">Whether to play the dodge animation.</param>
    protected virtual void UpdateAnimationStates(bool playIdleAnimation, bool playRightTurnAnimation, bool playLeftTurnAnimation, bool playDodgeAnimation)
    {
        _airplaneAnimationManager.PlayIdleAnimation(playIdleAnimation);
        _airplaneAnimationManager.PlayRightTurnAnimation(playRightTurnAnimation);
        _airplaneAnimationManager.PlayLeftTurnAnimation(playLeftTurnAnimation);
        _airplaneAnimationManager.PlayDodgeAnimation(playDodgeAnimation);
    }

    /// <summary>
    /// Initializes the default speed and propeller rate values.
    /// </summary>
    protected virtual void InitializeSpeed()
    {
        _defaultSpeed = _speed;
        _propellerDefaultRate = _externalSoundSource?.Pitch ?? 0;
    }

    /// <summary>
    /// Sets the speed and propeller rate values for the movement manager.
    /// </summary>
    /// <param name="newSpeed">The new speed value.</param>
    /// <param name="speedChangeTime">The time taken to change the speed.</param>
    /// <param name="propellerRate">The new propeller rate value.</param>
    /// <param name="propellerRateChangeTime">The time taken to change the propeller rate.</param>
    protected virtual void SetSpeed(float newSpeed, float speedChangeTime, float propellerRate, float propellerRateChangeTime)
    {
        _newSpeed = newSpeed;
        _speedChangeTime = speedChangeTime;
        _propellerNewRate = propellerRate;
        _propellerRateChangeTime = propellerRateChangeTime;
    }

    /// <summary>
    /// Coroutine that continuously updates the speed of the airplane.
    /// </summary>
    protected virtual IEnumerator UpdateSpeed()
    {
        while (true)
        {
            _speed = Mathf.Lerp(_speed, _newSpeed, _speedChangeTime * Time.fixedDeltaTime);         
            bool resetCameraSize = _newSpeed == _defaultSpeed;
            UpdateCameraSize(resetCameraSize);
            ControlEngineSound();
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }
    }

    /// <summary>
    /// Updates the camera size based on the current speed.
    /// </summary>
    /// <param name="reset">Whether to reset the camera size to default.</param>
    protected virtual void UpdateCameraSize(bool reset = false)
    {
        if (reset)
        {
            Reference.Manager.CameraSizeController.ResetOrtographicSizeToDefault();
            return;
        }

        Reference.Manager.CameraSizeController.UpdateOrtographicSize(10f, 0.2f);
    }

    /// <summary>
    /// Controls the engine sound based on the propeller rate.
    /// </summary>
    protected virtual void ControlEngineSound()
    {
        if(_externalSoundSource == null)
        {
            return;
        }

        _externalSoundSource.Pitch = Mathf.Lerp(_externalSoundSource.Pitch, _propellerNewRate, _propellerRateChangeTime * Time.fixedDeltaTime);
    }
}