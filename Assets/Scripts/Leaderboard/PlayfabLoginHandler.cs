using PlayFab;
using PlayFab.ClientModels;

public class PlayfabLoginHandler : BasePlayfabHandler<LoginWithPlayFabRequest, LoginResult>
{
    private readonly string _username;
    private readonly string _password;




    public PlayfabLoginHandler(string username, string password)
    {
        _request = new LoginWithPlayFabRequest { Username = _username = username, Password = _password = password };
        PlayFabClientAPI.LoginWithPlayFab(_request, OnSucceed, OnFailed);      
    }

    protected override void OnSucceed(LoginResult result)
    {
        _data[0] = _username;
        _data[1] = _password;
        GameEventHandler.RaiseEvent(GameEventType.UserLoginSucceed, _data);
    }

    protected override void OnFailed(PlayFabError error)
    {
        _data[0] = error.ErrorMessage;
        GameEventHandler.RaiseEvent(GameEventType.UserLoginFailed, _data);
    }
}
