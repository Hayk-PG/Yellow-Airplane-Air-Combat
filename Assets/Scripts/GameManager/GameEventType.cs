
public enum GameEventType 
{
    BackgroundSpritesInit,
    CloudSpritesInit,
    CrosshairSpriteInit,

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