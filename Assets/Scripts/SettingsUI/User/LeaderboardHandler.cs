using PlayFab.ClientModels;
using UnityEngine;
using Pautik;

public class LeaderboardHandler : BaseUserManager
{
    [Header("Canvas Group")]
    [SerializeField] private CanvasGroup[] _canvasGroups;

    [Header("Button")]
    [SerializeField] private Btn _leaderboardButton;
    [SerializeField] private Btn _closeButton;

    [Header("Leaderboard Row")]
    [SerializeField] private LeaderboardRow[] _rows;

    private PlayfabLeaderboardHandler _playfabLeaderboardHandler;
    private GetLeaderboardResult _leaderboardResult;
    private PlayerLeaderboardEntry _userLeaderboardEntry;

    private bool _isLeaderboardButtonClicked;




    protected override void OnEnable()
    {
        base.OnEnable();

        _leaderboardButton.OnSelect += OnLeaderboardButtonClick;
        _closeButton.OnSelect += () => SetLeaderboardActive(false);
    }

    private void OnLeaderboardButtonClick()
    {
        _isLeaderboardButtonClicked = true;

        if (!PlayfabLoginVerifier.IsLoggedIn)
        {
            GameEventHandler.RaiseEvent(GameEventType.RequestUserAuth);
            return;
        }

        if (_leaderboardResult == null)
        {
            InitializeLeaderboardHandler();
            return;
        }

        SetLeaderboardActive();
        PopulateLeaderboardRows();
    }

    protected override void OnGameEvent(GameEventType gameEventType, object[] data)
    {
        HandleAuthenticationSuccess(gameEventType, data);
        HandlerLeaderboardSuccess(gameEventType, data);
        HandleLeaderboardFailure(gameEventType, data);
        HandleLeaderboardForcedUpdate(gameEventType);
    }

    private void HandleAuthenticationSuccess(GameEventType gameEventType, object[] data)
    {
        if (gameEventType != GameEventType.UserRegistrationSucceed && gameEventType != GameEventType.UserLoginSucceed)
        {
            return;
        }

        InitializeLeaderboardHandler();
    }

    private void HandlerLeaderboardSuccess(GameEventType gameEventType, object[] data)
    {
        if (gameEventType != GameEventType.LeaderboardSucceed)
        {
            return;
        }

        _leaderboardResult = (GetLeaderboardResult)data[0];
        SetLeaderboardActive();
        PopulateLeaderboardRows();
    }

    private void HandleLeaderboardFailure(GameEventType gameEventType, object[] data)
    {
        if (gameEventType != GameEventType.LeaderboardFailed)
        {
            return;
        }

        SetLeaderboardActive(false);
    }

    private void HandleLeaderboardForcedUpdate(GameEventType gameEventType)
    {
        if (gameEventType != GameEventType.ForceLeaderboardUpdate)
        {
            return;
        }

        InitializeLeaderboardHandler();
    }

    private void InitializeLeaderboardHandler()
    {
        _playfabLeaderboardHandler = new PlayfabLeaderboardHandler();
    }

    private void SetLeaderboardActive(bool isActive = true)
    {
        foreach (var canvasGroup in _canvasGroups)
        {
            SetCanvasGroupActive(canvasGroup, isActive && _isLeaderboardButtonClicked);
        }
    }

    private void PopulateLeaderboardRows()
    {
        _userLeaderboardEntry = _leaderboardResult.Leaderboard.Find(user => user.DisplayName == ProfileData.Manager.Username);

        bool isUser = false;
        bool isUserInTopTen = false;

        for (int i = 0; i < 10; i++)
        {
            int position = i + 1;

            if (_leaderboardResult.Leaderboard.Count < position)
            {
                _rows[i].Display(rank: $"{position}.", name: "----", value: "--");

                continue;
            }

            isUser = _leaderboardResult.Leaderboard[i].DisplayName == ProfileData.Manager.Username;
            isUserInTopTen = isUser ? true: isUserInTopTen;
            position = _leaderboardResult.Leaderboard[i].Position + 1;

            _rows[i].Display(rank: $"{position}.", name: _leaderboardResult.Leaderboard[i].DisplayName, value: Converter.ThousandsSeparatorString(_leaderboardResult.Leaderboard[i].StatValue), isUser);
        }

        RenderLeaderboardWithUser(isUserInTopTen);
    }

    private void RenderLeaderboardWithUser(bool isUserInTopTen)
    {
        if (isUserInTopTen)
        {
            _rows[_rows.Length - 1].gameObject.SetActive(false);
            return;
        }

        int userPosition = _userLeaderboardEntry == null ? 100 : _userLeaderboardEntry.Position + 1;
        int value = _userLeaderboardEntry == null ? 0 : _userLeaderboardEntry.StatValue;

        _rows[_rows.Length - 1].gameObject.SetActive(true);
        _rows[_rows.Length - 1].Display(rank: userPosition < 100 ? $"{userPosition}." : "99+", name: ProfileData.Manager.Username, value: Converter.ThousandsSeparatorString(value), true);
    }
}