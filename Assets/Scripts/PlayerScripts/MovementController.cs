using UnityEngine;


public class MovementController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private Animator _animator;

    [Header("Speed")]
    [SerializeField] private float _speed;




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
    }
}