using Pautik;

public class UserAuthManager : BaseUserManager
{
    private void Start()
    {
        if (ProfileData.Manager.IsCreatingAccountPromptDisabled)
        {
            return;
        }

        if (PlayfabLoginVerifier.IsLoggedIn)
        {
            return;
        }

        RequestAuth();
    }

    protected override void OnGameEvent(GameEventType gameEventType, object[] data)
    {
        HandleUserAuthRequest(gameEventType, data);
        HandleLogoutRequest(gameEventType, data);
    }

    private void HandleUserAuthRequest(GameEventType gameEventType, object[] data)
    {
        if (gameEventType != GameEventType.RequestUserAuth)
        {
            return;
        }

        RequestAuth();
    }

    private void HandleLogoutRequest(GameEventType gameEventType, object[] data)
    {
        if (gameEventType != GameEventType.RequestUserLogout)
        {
            return;
        }

        ProfileData.Manager.ResetToDefault();
        RequestAuth();
    }

    private void RequestAuth()
    {
        Conditions<bool>.Compare(IsAutoLoginEnabled, () => GameEventHandler.RaiseEvent(GameEventType.RequestUserLogin), () => GameEventHandler.RaiseEvent(GameEventType.RequestUserRegistration));
    }
}