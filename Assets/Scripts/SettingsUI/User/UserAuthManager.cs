using Pautik;

public class UserAuthManager : BaseUserManager
{
    protected override void OnGameEvent(GameEventType gameEventType, object[] data)
    {
        HandleMainMenuInitialization(gameEventType);
        HandleUserAuthRequest(gameEventType, data);
        HandleLogoutRequest(gameEventType, data);
    }

    private void HandleMainMenuInitialization(GameEventType gameEventType)
    {
        if (gameEventType != GameEventType.OnMainMenuInit)
        {
            return;
        }

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