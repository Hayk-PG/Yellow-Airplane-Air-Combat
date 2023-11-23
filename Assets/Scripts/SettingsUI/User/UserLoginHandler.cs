using Pautik;

public class UserLoginHandler : BaseUserSignupManager
{
    private PlayfabLoginHandler _playfabLoginHandler;

    private bool _isAutoLoginToggleOn;




    protected override void OnEnable()
    {
        base.OnEnable();
        _buttons[1].OnSelect += OnCreateAccountButtonClick;
        _buttons[2].OnSelect += OnForgotPasswordButtonClick;
        _buttons[3].OnSelect += CleanClose;
        _toggles[0].OnValueChange += OnAutoLoginToggleValueChanged;
    }

    protected override void HandleRequest(GameEventType gameEventType, object[] data)
    {
        if (gameEventType != GameEventType.RequestUserLogin)
        {
            return;
        }

        ResetToDefault();

        Conditions<bool>.Compare(IsAutoLoginEnabled, () => RequestPlayfabLogin(ProfileData.Manager.SavedUsername, ProfileData.Manager.SavedPassword), () => 
        {
            SetCanvasGroupActive(_canvasGroups[0]);
            SetCanvasGroupActive(_canvasGroups[1]);
        });
    }

    protected override void HandleOperationSuccess(GameEventType gameEventType, object[] data)
    {
        if (gameEventType != GameEventType.UserLoginSucceed)
        {
            return;
        }

        CleanClose();
        SaveUserCredentials(username: (string)data[0], password: (string)data[1]);
        ProfileData.Manager.CacheUserCredentials(username: (string)data[0], password: (string)data[1]);
    }

    protected override void HandleOperationFailure(GameEventType gameEventType, object[] data)
    {
        if (gameEventType != GameEventType.UserLoginFailed)
        {
            return;
        }

        ResetToDefault();
        SetCanvasGroupActive(_canvasGroups[0]);
        SetCanvasGroupActive(_canvasGroups[1]);
    }

    protected override void OnMainButtonClick()
    {
        base.OnMainButtonClick();
        RequestPlayfabLogin(Username, Password);
        DeleteUserCredentials();
    }

    private void OnCreateAccountButtonClick()
    {
        CleanClose();
        GameEventHandler.RaiseEvent(GameEventType.RequestUserRegistration);
    }

    private void OnForgotPasswordButtonClick()
    {
        CleanClose();
        GameEventHandler.RaiseEvent(GameEventType.RequestAccountRecover);       
    }

    private void CleanClose()
    {
        ResetToDefault();
        SetCanvasGroupActive(_canvasGroups[0], false);
        SetCanvasGroupActive(_canvasGroups[1], false);
    }

    private void OnAutoLoginToggleValueChanged(bool isOn)
    {
        _isAutoLoginToggleOn = isOn;
    }

    private void RequestPlayfabLogin(string username, string password)
    {
        _playfabLoginHandler = new PlayfabLoginHandler(username, password);
    }

    private void SaveUserCredentials(string username, string password)
    {
        if (!_isAutoLoginToggleOn)
        {
            return;
        }

        ProfileData.Manager.SaveOrDeleteUserCredentials(username: username, password: password); 
    }

    private void DeleteUserCredentials()
    {
        if (_isAutoLoginToggleOn)
        {
            return;
        }

        ProfileData.Manager.SaveOrDeleteUserCredentials(null, null, true);
    }
}