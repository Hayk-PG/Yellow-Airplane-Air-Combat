using PlayFab.ClientModels;
using UnityEngine;

public class LeaderboardHandler : BaseUserManager
{
    [Header("Canvas Group")]
    [SerializeField] private CanvasGroup[] _canvasGroups;

    [Header("User Button")]
    [SerializeField] private Btn _leaderboardButton;

    private PlayfabLeaderboardHandler _playfabLeaderboardHandler;
    private GetLeaderboardResult _leaderboardResult;
    private string _username;




    protected override void OnEnable()
    {
        base.OnEnable();

        _leaderboardButton.OnSelect += OnLeaderboardButtonClick;
    }

    private void OnLeaderboardButtonClick()
    {
        if (!PlayfabLoginVerifier.IsLoggedIn)
        {
            GameEventHandler.RaiseEvent(GameEventType.RequestUserAuth);
            return;
        }

        SetLeaderboardActive();
    }

    protected override void OnGameEvent(GameEventType gameEventType, object[] data)
    {
        HandleAuthenticationSuccess(gameEventType, data);
        HandlerLeaderboardSuccess(gameEventType, data);
        HandleLeaderboardFailure(gameEventType, data);
    }

    private void HandleAuthenticationSuccess(GameEventType gameEventType, object[] data)
    {
        if (gameEventType != GameEventType.UserRegistrationSucceed && gameEventType != GameEventType.UserLoginSucceed)
        {
            return;
        }

        _username = (string)data[0];
        _playfabLeaderboardHandler = new PlayfabLeaderboardHandler();
    }

    private void HandlerLeaderboardSuccess(GameEventType gameEventType, object[] data)
    {
        if (gameEventType != GameEventType.LeaderboardSucceed)
        {
            return;
        }

        _leaderboardResult = (GetLeaderboardResult)data[0];

        SetLeaderboardActive();
    }

    private void HandleLeaderboardFailure(GameEventType gameEventType, object[] data)
    {
        if (gameEventType != GameEventType.LeaderboardFailed)
        {
            return;
        }

        print((string)data[0]);
    }

    private void SetLeaderboardActive(bool isActive = true)
    {
        bool isActiveCheck = _leaderboardResult != null ? isActive : false;

        print($"Leaderboard activity: {isActiveCheck}");

        foreach (var canvasGroup in _canvasGroups)
        {
            SetCanvasGroupActive(canvasGroup, isActiveCheck);
        }

        if (!isActiveCheck)
        {
            return;
        }

        foreach (var leaderboard in _leaderboardResult.Leaderboard)
        {
            print($"Position: {leaderboard.Position}/Name: {leaderboard.DisplayName }");
        }
    }
}