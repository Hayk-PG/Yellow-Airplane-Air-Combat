using PlayFab.ClientModels;
using UnityEngine;
using Pautik;

public class LeaderboardHandler : BaseUserManager
{
    [Header("Canvas Group")]
    [SerializeField] private CanvasGroup[] _canvasGroups;

    [Header("User Button")]
    [SerializeField] private Btn _leaderboardButton;

    private PlayfabLeaderboardHandler _playfabLeaderboardHandler;
    private bool _isLeaderboardButtonClicked;




    protected override void OnEnable()
    {
        base.OnEnable();

        _leaderboardButton.OnSelect += OnLeaderboardButtonClick;
    }

    private void OnLeaderboardButtonClick()
    {
        _isLeaderboardButtonClicked = true;

        Conditions<bool>.Compare(PlayfabLoginVerifier.IsLoggedIn, ()=> SetLeaderboardActive(), () => GameEventHandler.RaiseEvent(GameEventType.RequestUserAuth));
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

        _playfabLeaderboardHandler = new PlayfabLeaderboardHandler();
    }

    private void HandlerLeaderboardSuccess(GameEventType gameEventType, object[] data)
    {
        if (gameEventType != GameEventType.LeaderboardSucceed)
        {
            return;
        }

        ProfileData.Manager.CacheLeaderboardResult(leaderboardResult: (GetLeaderboardResult)data[0]);
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
        bool isActiveCheck = ProfileData.Manager.LeaderboardResult != null ? isActive : false;

        print($"Leaderboard activity: {isActiveCheck}");

        foreach (var canvasGroup in _canvasGroups)
        {
            SetCanvasGroupActive(canvasGroup, isActiveCheck && _isLeaderboardButtonClicked);
        }

        if (!isActiveCheck)
        {
            return;
        }

        foreach (var leaderboard in ProfileData.Manager.LeaderboardResult.Leaderboard)
        {
            print($"Position: {leaderboard.Position}/Name: {leaderboard.DisplayName }");
        }
    }
}