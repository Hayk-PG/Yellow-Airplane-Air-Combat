using Pautik;

public class UserRegistrationHandler : BaseUserSignupManager
{
    private PlayfabRegistrationHandler _playfabResgistration;

    private const string _commonErrorMessage = "The display name entered is not available.";
    private const string _invalidCharacterError = "Username contains invalid characters.";




    protected override void OnEnable()
    {
        base.OnEnable();
        _buttons[1].OnSelect += OnLoginButtonClick;
        _buttons[2].OnSelect += CleanClose;
        _toggles[0].OnValueChange += OnAskPromptToggleValueChanged;
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

        CleanClose();
        SoundOverrider.Success();
        ProfileData.Manager.CacheUserCredentials(username: (string)data[0], password: (string)data[1], email: data[2] != null ? (string)data[2] : null);
    }

    protected override void HandleOperationFailure(GameEventType gameEventType, object[] data)
    {
        if (gameEventType != GameEventType.UserRegistrationFailed)
        {
            return;
        }

        ResetToDefault();
        SetCanvasGroupActive(_canvasGroups[0]);
        SetCanvasGroupActive(_canvasGroups[1]);
        SoundOverrider.Fail();
        Conditions<bool>.Compare(data[1] == null, () => UpdateTitle(text: (string)data[0] == _commonErrorMessage ? _errors[0] : _errors[2]), ()=> UpdateTitle(text: (string)data[1] == _invalidCharacterError ? _errors[3] : _errors[2]));      
    }

    private void OnLoginButtonClick()
    {
        CleanClose();
        GameEventHandler.RaiseEvent(GameEventType.RequestUserLogin);
    }

    private void CleanClose()
    {
        ResetToDefault();
        SetCanvasGroupActive(_canvasGroups[0], false);
        SetCanvasGroupActive(_canvasGroups[1], false);
    }

    private void OnAskPromptToggleValueChanged(bool isOn)
    {
        ProfileData.Manager.SetAccountCreationAskPromptState(isOn);
    }

    protected override void OnMainButtonClick()
    {
        base.OnMainButtonClick();
        ProfileData.Manager.SaveOrDeleteUserCredentials(null, null, true);

        _playfabResgistration = new PlayfabRegistrationHandler(Email, Username, Password);
    }
}