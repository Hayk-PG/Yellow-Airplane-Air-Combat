using UnityEngine;
using Pautik;
using System.Collections;

public class MovementController : MonoBehaviour
{
    private enum AnimationState { Idle, TurnRight, TurnLeft, Dodge}

    [Header("Components")]
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AirplaneAnimationManager _airplaneAnimationManager;
    [SerializeField] private ExternalSoundSource _externalSoundSource;

    private Quaternion _previousRotation;
    private Quaternion _currentRotation;

    [Header("Speed")]
    [SerializeField] private float _speed;

    private float _defaultSpeed;
    private float _newSpeed;
    private float _speedChangeTime;
    private float _propellerDefaultRate;
    private float _propellerNewRate;
    private float _propellerRateChangeTime;




    private void Start()
    {
        InitializeSpeed();
        InitializePreviousRotation();
        StartCoroutine(UpdateSpeed());
    }

    private void OnEnable()
    {
        Reference.Manager.InputController.OnInputController += OnInputController;
    }

    private void FixedUpdate() 
    {
        Move();
    }

    // Event handler for the OnInputController event
    private void OnInputController(InputController.InputType inputType, object[] data)
    {
        Rotate(inputType, data);
        HandleJoystickRelease(inputType);
    }

    // Move the object
    private void Move() 
    {
        // Calculate the velocity based on the object's right direction and speed
        Vector2 velocity = transform.right * _speed * Time.fixedDeltaTime;

        // Apply the velocity to the rigidbody
        _rigidbody.velocity = velocity;
    }

    // Rotate the object based on input
    private void Rotate(InputController.InputType inputType, object[] data) 
    {
        // Only rotate if the input type is MoveJoystick
        if (inputType != InputController.InputType.MoveJoystick)
        {
            return;
        }

        // Extract the joystick input vector from the data array
        Vector2 joystickInput = (Vector2)data[0];

        // Calculate the angle between the joystick input and the x-axis in degrees
        float angle = Mathf.Atan2(joystickInput.y, joystickInput.x) * Mathf.Rad2Deg;

        // Smoothly rotate the object towards the calculated angle using Slerp
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, angle), 2 * Time.deltaTime);

        UpdateRotationDirection();
    }

    // Handles the release of the joystick.
    private void HandleJoystickRelease(InputController.InputType inputType)
    {
        if(inputType != InputController.InputType.ReleaseJoystick)
        {
            return;
        }

        SetAnimationState(AnimationState.Idle);
        SetSpeed(_defaultSpeed, 3f, _propellerDefaultRate, 1f);
    }

    private void InitializePreviousRotation()
    {
        _previousRotation = transform.rotation;
    }

    // Updates the rotation direction based on the current and previous rotations.
    private void UpdateRotationDirection()
    {
        _currentRotation = transform.rotation;

        float angleDifference = Converter.GetAngleDifference(_currentRotation.eulerAngles.z, _previousRotation.eulerAngles.z);

        if(angleDifference > 0f)
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

    // Sets the animation state based on the specified animation state.
    private void SetAnimationState(AnimationState animationState)
    {
        switch (animationState)
        {
            case AnimationState.Idle: UpdateAnimationStates(true, false, false, false); break;
            case AnimationState.TurnRight: UpdateAnimationStates(false, true, false, false); break;
            case AnimationState.TurnLeft: UpdateAnimationStates(false, false, true, false); break;
            case AnimationState.Dodge: UpdateAnimationStates(false, false, false, true); break;
        }
    }

    // Updates the animation states based on the specified values.
    private void UpdateAnimationStates(bool playIdleAnimation, bool playRightTurnAnimation, bool playLeftTurnAnimation, bool playDodgeAnimation)
    {
        _airplaneAnimationManager.PlayIdleAnimation(playIdleAnimation);
        _airplaneAnimationManager.PlayRightTurnAnimation(playRightTurnAnimation);
        _airplaneAnimationManager.PlayLeftTurnAnimation(playLeftTurnAnimation);     
        _airplaneAnimationManager.PlayDodgeAnimation(playDodgeAnimation);
    }

    // Initializes the default speed and propeller rate.
    private void InitializeSpeed()
    {
        _defaultSpeed = _speed;
        _propellerDefaultRate = _externalSoundSource.Pitch;
    }

    // Sets the new speed and propeller rate with the specified values and change times.
    private void SetSpeed(float newSpeed, float speedChangeTime, float propellerRate, float propellerRateChangeTime)
    {
        _newSpeed = newSpeed;
        _speedChangeTime = speedChangeTime;
        _propellerNewRate = propellerRate;
        _propellerRateChangeTime = propellerRateChangeTime;
    }

    // Coroutine that continuously updates the speed and propeller rate.
    private IEnumerator UpdateSpeed()
    {
        while (true)
        {
            _speed = Mathf.Lerp(_speed, _newSpeed, _speedChangeTime * Time.fixedDeltaTime);
            _externalSoundSource.Pitch = Mathf.Lerp(_externalSoundSource.Pitch, _propellerNewRate, _propellerRateChangeTime * Time.fixedDeltaTime);

            bool resetCameraSize = _newSpeed == _defaultSpeed;

            UpdateCameraSize(resetCameraSize);

            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }
    }

    private void UpdateCameraSize(bool reset = false)
    {
        if (reset)
        {
            Reference.Manager.CameraSizeController.ResetOrtographicSizeToDefault();
            return;
        }

        Reference.Manager.CameraSizeController.UpdateOrtographicSize(10f, 0.2f);
    }
}