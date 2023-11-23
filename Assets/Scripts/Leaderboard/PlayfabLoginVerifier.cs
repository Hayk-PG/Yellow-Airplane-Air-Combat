
public struct PlayfabLoginVerifier 
{
    public static bool IsLoggedIn => PlayFab.PlayFabClientAPI.IsClientLoggedIn();
}