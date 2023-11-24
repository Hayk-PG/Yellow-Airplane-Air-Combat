
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

    UserRegistrationSucceed,
    UserRegistrationFailed,
    UserLoginSucceed,
    UserLoginFailed,
    UserAccountRecoverySucceed,
    UserAccountRecoveryFailed,
    LeaderboardSucceed,
    LeaderboardFailed,   

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