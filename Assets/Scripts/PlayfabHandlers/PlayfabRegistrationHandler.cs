using PlayFab;
using PlayFab.ClientModels;
using Pautik;

public class PlayfabRegistrationHandler : BasePlayfabHandler<RegisterPlayFabUserRequest, RegisterPlayFabUserResult>
{
    private readonly string _email;
    private readonly string _username;
    private readonly string _password;
    private const string _nameNotAvailableError = "The display name entered is not available.";




    public PlayfabRegistrationHandler(string email, string username, string password)
    {
        if (username == null || password == null)
        {
            return;
        }

        _request = new RegisterPlayFabUserRequest();
        _request.RequireBothUsernameAndEmail = false;

        if (!string.IsNullOrEmpty(email))
        {
            _request.Email = _email = email;
        }

        _request.DisplayName = _username = username;
        _request.Username = username;      
        _request.Password = _password = password;
     
        PlayFabClientAPI.RegisterPlayFabUser(_request, OnSucceed, OnFailed);
        GameEventHandler.RaiseEvent(GameEventType.UserRegistrationProceed);
    }

    protected override void OnSucceed(RegisterPlayFabUserResult result)
    {
        result.PlayFabId = _username;
    
        _data[0] = _username;
        _data[1] = _password;
        _data[2] = _email;
        GameEventHandler.RaiseEvent(GameEventType.UserRegistrationSucceed, _data);
    }

    protected override void OnFailed(PlayFabError error)
    {
        _data[0] = error.ErrorMessage;
        GameEventHandler.RaiseEvent(GameEventType.UserRegistrationFailed, _data);        
    }
}