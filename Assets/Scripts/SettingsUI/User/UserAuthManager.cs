
public class UserAuthManager : BaseUserManager
{
    private bool IsAutoLoginEnabled { get; set; }




    protected override void OnGameEvent(GameEventType gameEventType, object[] data)
    {
        HandleUserAuthRequest(gameEventType, data);
    }

    private void HandleUserAuthRequest(GameEventType gameEventType, object[] data)
    {
        if (gameEventType != GameEventType.RequestUserAuth)
        {
            return;
        }

        if (IsAutoLoginEnabled)
        {
            GameEventHandler.RaiseEvent(GameEventType.RequestUserLogin);
            return;
        }
        print("a");
        GameEventHandler.RaiseEvent(GameEventType.RequestUserRegistration);
    }
}