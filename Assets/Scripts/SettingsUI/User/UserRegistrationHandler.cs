
public class UserRegistrationHandler : BaseUserSignupManager
{
    private PlayfabRegistrationHandler _playfabResgistration;




    protected override void OnEnable()
    {
        base.OnEnable();
        _buttons[1].OnSelect += OnLoginButtonClick;
    }

    protected override void HandleRequest(GameEventType gameEventType, object[] data)
    {
        if (gameEventType != GameEventType.RequestUserRegistration)
        {
            return;
        }

        ResetToDefault();
        SetCanvasGroupActive(_canvasGroups[0]);
        SetCanvasGroupActive(_canvasGroups[1]);
    }

    protected override void HandleOperationSuccess(GameEventType gameEventType, object[] data)
    {
        if (gameEventType != GameEventType.UserRegistrationSucceed)
        {
            return;
        }

        ResetToDefault();
        SetCanvasGroupActive(_canvasGroups[0], false);
        SetCanvasGroupActive(_canvasGroups[1], false);
    }

    protected override void HandleOperationFailure(GameEventType gameEventType, object[] data)
    {
        if (gameEventType != GameEventType.UserRegistrationFailed)
        {
            return;
        }

        ResetToDefault();
        ToggleCurrentTabInteractability();

        print($"Failed to create an account!: {(string)data[0]}");
    }

    private void OnLoginButtonClick()
    {
        ResetToDefault();
        SetCanvasGroupActive(_canvasGroups[0], false);
        SetCanvasGroupActive(_canvasGroups[1], false);

        GameEventHandler.RaiseEvent(GameEventType.RequestUserLogin);
    }

    protected override void OnMainButtonClick()
    {
        base.OnMainButtonClick();
        _playfabResgistration = new PlayfabRegistrationHandler(Email, Username, Password);
    }
}