using UnityEngine;
using Pautik;


public class MovementController : MonoBehaviour
{
    private enum AnimationState { Idle, TurnRight, TurnLeft, Dodge}

    [Header("Components")]
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AirplaneAnimationManager _airplaneAnimationManager;

    private Quaternion _previousRotation;
    private Quaternion _currentRotation;

    [Header("Speed")]
    [SerializeField] private float _speed;




    private void Start()
    {
        _previousRotation = transform.rotation;
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
    }

    // Updates the rotation direction based on the current and previous rotations.
    private void UpdateRotationDirection()
    {
        _currentRotation = transform.rotation;

        float angleDifference = Converter.GetAngleDifference(_currentRotation.eulerAngles.z, _previousRotation.eulerAngles.z);

        if(angleDifference > 0f)
        {
            SetAnimationState(AnimationState.TurnLeft);         
        }

        else if (angleDifference < 0f)
        {
            SetAnimationState(AnimationState.TurnRight);
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
}