using System.Collections;
using UnityEngine;
using Pautik;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AirplaneAnimationManager))]
public abstract class BaseAirplaneMovementManager : MonoBehaviour
{
    protected enum AnimationState { Idle, TurnRight, TurnLeft, Dodge }

    [Header("Components")]
    [SerializeField] protected Rigidbody2D _rigidbody;
    [SerializeField] protected AirplaneAnimationManager _airplaneAnimationManager;
    [SerializeField] protected ExternalSoundSource _externalSoundSource;

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




    protected virtual void Start()
    {
        InitializeSpeed();
        InitializePreviousRotation();
        StartCoroutine(UpdateSpeed());
    }

    protected virtual void Move(Vector2 velocity)
    {
        _rigidbody.velocity = velocity;
    }

    protected virtual void Rotate(float angle)
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, angle), _rotationSpeed * Time.deltaTime);
    }

    protected Vector2 Velocity(Vector2 direction, float speed)
    {
        return direction * speed * Time.fixedDeltaTime;
    }

    protected virtual float Angle(Vector2 direction)
    {
        return Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    }

    protected virtual void InitializePreviousRotation()
    {
        _previousRotation = transform.rotation;
    }

    protected virtual void UpdateRotationDirection()
    {
        _currentRotation = transform.rotation;

        float angleDifference = Converter.GetAngleDifference(_currentRotation.eulerAngles.z, _previousRotation.eulerAngles.z);

        if (angleDifference > 0f)
        {
            SetAnimationState(AnimationState.TurnLeft);
            SetSpeed(_defaultSpeed * 1.5f, 1f, 1.5f, 0.2f);
        }

        else if (angleDifference < 0f)
        {
            SetAnimationState(AnimationState.TurnRight);
            SetSpeed(_defaultSpeed * 1.5f, 1f, 1.5f, 0.2f);
        }

        _previousRotation = _currentRotation;
    }

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

    protected virtual void UpdateAnimationStates(bool playIdleAnimation, bool playRightTurnAnimation, bool playLeftTurnAnimation, bool playDodgeAnimation)
    {
        _airplaneAnimationManager.PlayIdleAnimation(playIdleAnimation);
        _airplaneAnimationManager.PlayRightTurnAnimation(playRightTurnAnimation);
        _airplaneAnimationManager.PlayLeftTurnAnimation(playLeftTurnAnimation);
        _airplaneAnimationManager.PlayDodgeAnimation(playDodgeAnimation);
    }

    protected virtual void InitializeSpeed()
    {
        _defaultSpeed = _speed;
        _propellerDefaultRate = _externalSoundSource?.Pitch ?? 0;
    }

    protected virtual void SetSpeed(float newSpeed, float speedChangeTime, float propellerRate, float propellerRateChangeTime)
    {
        _newSpeed = newSpeed;
        _speedChangeTime = speedChangeTime;
        _propellerNewRate = propellerRate;
        _propellerRateChangeTime = propellerRateChangeTime;
    }

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

    protected virtual void UpdateCameraSize(bool reset = false)
    {
        if (reset)
        {
            Reference.Manager.CameraSizeController.ResetOrtographicSizeToDefault();
            return;
        }

        Reference.Manager.CameraSizeController.UpdateOrtographicSize(10f, 0.2f);
    }

    protected virtual void ControlEngineSound()
    {
        if(_externalSoundSource == null)
        {
            return;
        }

        _externalSoundSource.Pitch = Mathf.Lerp(_externalSoundSource.Pitch, _propellerNewRate, _propellerRateChangeTime * Time.fixedDeltaTime);
    }
}