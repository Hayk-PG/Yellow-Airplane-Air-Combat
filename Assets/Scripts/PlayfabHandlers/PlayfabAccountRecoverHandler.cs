using PlayFab;
using PlayFab.ClientModels;

public class PlayfabAccountRecoverHandler : BasePlayfabHandler<SendAccountRecoveryEmailRequest, SendAccountRecoveryEmailResult>
{
    private readonly string _email;




    public PlayfabAccountRecoverHandler(string email)
    {
        _request = new SendAccountRecoveryEmailRequest { Email = _email = email, TitleId = "F6E9E" };
        PlayFabClientAPI.SendAccountRecoveryEmail(_request, OnSucceed, OnFailed);
    }

    protected override void OnSucceed(SendAccountRecoveryEmailResult result)
    {
        _data[0] = _email;
        GameEventHandler.RaiseEvent(GameEventType.UserAccountRecoverySucceed, _data);
    }

    protected override void OnFailed(PlayFabError error)
    {
        _data[0] = error.ErrorMessage;
        GameEventHandler.RaiseEvent(GameEventType.UserAccountRecoveryFailed, _data);
    }
}