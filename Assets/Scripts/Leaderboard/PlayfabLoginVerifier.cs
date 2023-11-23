
public struct PlayfabLoginVerifier 
{
    public static bool IsLoggedIn => PlayFab.PlayFabClientAPI.IsClientLoggedIn() && !string.IsNullOrEmpty(ProfileData.Manager.Username) && !string.IsNullOrEmpty(ProfileData.Manager.Password);
}