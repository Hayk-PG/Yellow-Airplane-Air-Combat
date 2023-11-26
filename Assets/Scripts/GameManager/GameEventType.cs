
public enum GameEventType 
{
    BackgroundSpritesInit,
    CloudSpritesInit,
    CrosshairSpriteInit,
    AirplaneAssetInit,

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

    AirplaneRendererCreated,
    AirplaneFrameChanged,
    AnimateAirplane,
    UpdateAirplaneColor,
    MuzzleFlashTriggered,

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