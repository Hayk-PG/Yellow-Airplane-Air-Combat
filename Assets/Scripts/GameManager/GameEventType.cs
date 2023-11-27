
public enum GameEventType 
{
    RequestUserAuth,    
    RequestUserRegistration,
    RequestUserLogin,
    RequestAccountRecover,
    RequestUserLogout,

    UserRegistrationProceed,
    UserLoginProceed,
    LeaderboardProceed,
    UserStatisticsUpdateProceed,

    UserRegistrationSucceed,
    UserRegistrationFailed,
    UserLoginSucceed,
    UserLoginFailed,
    UserAccountRecoverySucceed,
    UserAccountRecoveryFailed,
    LeaderboardSucceed,
    LeaderboardFailed,
    UserStatisticsUpdateSucceed,
    UserStatisticsUpdateFailed,
    ForceLeaderboardUpdate,

    DisplayInterstitialAd,

    OnMainMenuInit,
    OnLastHopeDefenderMessageActivity,
    OnJoystickMove,
    OnJoystickRelease,
    OnShootButtonState,
    OnScored,
    OnPlayerAirplaneDestroy,
    OnGameOverScreenFinalize,
    PlayerMoveBroadcast,
    OnPauseButtonClick,
    OnResumeButtonClick,
    ResetGameSpeed,
    OnGunfireDefaultRateInit,
    OnGunFire
}