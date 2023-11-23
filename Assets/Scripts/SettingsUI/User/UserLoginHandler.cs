
public class UserLoginHandler : BaseUserSignupManager
{
    private PlayfabLoginHandler _playfabLoginHandler;





    protected override void OnEnable()
    {
        base.OnEnable();
        _buttons[1].OnSelect += OnCreateAccountButtonClick;
        _buttons[2].OnSelect += OnForgotPasswordButtonClick;
    }

    protected override void HandleRequest(GameEventType gameEventType, object[] data)
    {
        if (gameEventType != GameEventType.RequestUserLogin)
        {
            return;
        }

        ResetToDefault();
        SetCanvasGroupActive(_canvasGroups[0]);
        SetCanvasGroupActive(_canvasGroups[1]);
    }

    protected override void HandleOperationSuccess(GameEventType gameEventType, object[] data)
    {
        if (gameEventType != GameEventType.UserLoginSucceed)
        {
            return;
        }

        ResetToDefault();
        SetCanvasGroupActive(_canvasGroups[0], false);
        SetCanvasGroupActive(_canvasGroups[1], false);
    }

    protected override void HandleOperationFailure(GameEventType gameEventType, object[] data)
    {
        if (gameEventType != GameEventType.UserLoginFailed)
        {
            return;
        }

        ResetToDefault();
        ToggleCurrentTabInteractability();

        print($"Failed to log in!: {(string)data[0]}");
    }

    protected override void OnMainButtonClick()
    {
        base.OnMainButtonClick();
        _playfabLoginHandler = new PlayfabLoginHandler(Username, Password);
    }

    private void OnCreateAccountButtonClick()
    {
        ResetToDefault();
        SetCanvasGroupActive(_canvasGroups[0], false);
        SetCanvasGroupActive(_canvasGroups[1], false);

        GameEventHandler.RaiseEvent(GameEventType.RequestUserRegistration);
    }

    private void OnForgotPasswordButtonClick()
    {
        ResetToDefault();
        SetCanvasGroupActive(_canvasGroups[0], false);
        SetCanvasGroupActive(_canvasGroups[1], false);

        GameEventHandler.RaiseEvent(GameEventType.RequestAccountRecover);       
    }
}