using UnityEngine;

public class InputController : MonoBehaviour
{
    [Header("Joystick")]
    [SerializeField] private FixedJoystick _fixedJoystick; 

    [Header("Shoot Button")]
    [SerializeField] private Btn _shootButton; 
   
    private object[] _joystickInputData = new object[1];
    private bool _isJoystickReleaseEventInvoked;
    private bool _isShootButtonHeld;

    private bool IsJoystickReleased
    {
        get => Mathf.Abs(_fixedJoystick.Direction.x) < 0.1f && Mathf.Abs(_fixedJoystick.Direction.y) < 0.1f;
    }




    private void OnEnable()
    {
        _shootButton.OnPointerDownHandler += ()=> UpdateShootButtonHoldState(true);
        _shootButton.OnPointerUpHandler += () => UpdateShootButtonHoldState(false);
    }

    private void Update()
    {
        OnJoystickMove();
        HandleShootButtonHold();
    }

    private void OnJoystickMove()
    {
        if (IsJoystickReleased)
        {
            OnJoystickRelease();
            return;
        }

        RegisterAndPublishJoystickInputData(GameEventType.OnJoystickMove, _fixedJoystick.Direction);
        _isJoystickReleaseEventInvoked = false;
    }

    private void OnJoystickRelease()
    {
        if (_isJoystickReleaseEventInvoked)
        {
            return;
        }

        RegisterAndPublishJoystickInputData(GameEventType.OnJoystickRelease, _fixedJoystick.Direction);
        _isJoystickReleaseEventInvoked = true;
    }

    private void HandleShootButtonHold()
    {
        if (!_isShootButtonHeld)
        {
            return;
        }

        RegisterAndPublishJoystickInputData(GameEventType.OnShootButtonState, true);
    }

    public void UpdateShootButtonHoldState(bool isShootButtonHeld)
    {
        _isShootButtonHeld = isShootButtonHeld;
        RegisterAndPublishJoystickInputData(GameEventType.OnShootButtonState, isShootButtonHeld);
    }

    private void RegisterAndPublishJoystickInputData(GameEventType gameEventType, Vector2 direction)
    {
        _joystickInputData[0] = direction;
        GameEventHandler.RaiseEvent(gameEventType, _joystickInputData);
    }

    private void RegisterAndPublishJoystickInputData(GameEventType gameEventType, bool isShootButtonHeld)
    {
        _joystickInputData[0] = isShootButtonHeld;
        GameEventHandler.RaiseEvent(gameEventType, _joystickInputData);
    }
}