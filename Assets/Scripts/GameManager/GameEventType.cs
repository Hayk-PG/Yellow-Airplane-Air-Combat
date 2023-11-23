
public enum GameEventType 
{
    RequestUserAuth,    
    RequestUserRegistration,
    RequestUserLogin,
    RequestAccountRecover,

    UserRegistrationProceed,
    UserLoginProceed,

    UserRegistrationSucceed,
    UserRegistrationFailed,
    UserLoginSucceed,
    UserLoginFailed,
    UserAccountRecoverySucceed,
    UserAccountRecoveryFailed,

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