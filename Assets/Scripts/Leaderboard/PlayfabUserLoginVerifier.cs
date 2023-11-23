
public struct PlayfabUserLoginVerifier 
{
    public static bool IsLoggedIn => PlayFab.PlayFabClientAPI.IsClientLoggedIn();
}