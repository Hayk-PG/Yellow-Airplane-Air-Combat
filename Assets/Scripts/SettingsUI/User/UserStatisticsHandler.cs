using UnityEngine;
using Pautik;
using TMPro;

public class UserStatisticsHandler : BaseUserManager
{
    [Header("Canvas Group")]
    [SerializeField] private CanvasGroup[] _canvasGroups;

    [Header("Text")]
    [SerializeField] private TMP_Text _loginReminderMessage;

    [Header("Button")]
    [SerializeField] private Btn[] _buttons;

    [Header("Toggle")]
    [SerializeField] private CustomToggle[] _toggles;

    private PlayfabStatsUpdater _playfabStatsUpdater;
    private string _message = GlobalFunctions.TextWithColorCode("#FF005B","To update your score on the leaderboard and compete with other players, you need to be logged in. ") +
                              GlobalFunctions.WhiteColorText("Would you like to log in now and showcase your gaming prowess?");

    private int? _score;




    protected override void OnEnable()
    {
        base.OnEnable();

        _buttons[0].OnSelect += OnLoginButtonClick;
        _buttons[1].OnSelect += OnCloseButtonClick;

        _toggles[0].OnValueChange += OnDontAskAgainToggleValueChange;
    }

    protected override void OnGameEvent(GameEventType gameEventType, object[] data)
    {
        HandleGameOverScreenFinalization(gameEventType, data);
        HandleStatisticsUpdateSuccess(gameEventType);
        HandleStatisticsUpdateFailure(gameEventType, data);
        UpdateQueuedScoreOnRegistrationSucceed(gameEventType);
        UpdateQueuedScoreOnLoginSucceed(gameEventType);
    }

    private void HandleGameOverScreenFinalization(GameEventType gameEventType, object[] data)
    {
        if (gameEventType != GameEventType.OnGameOverScreenFinalize)
        {
            return;
        }

        _score = (int)data[0];

        if (!PlayfabLoginVerifier.IsLoggedIn)
        {
            if (ProfileData.Manager.IsLoginReminderDisabled)
            {
                return;
            }

            SetScreenActiveAndUpdateMessage(true);
            return;
        }

        RequestStatisticsUpdate();
    }

    private void HandleStatisticsUpdateSuccess(GameEventType gameEventType)
    {
        if (gameEventType != GameEventType.UserStatisticsUpdateSucceed)
        {
            return;
        }

        print($"Update statistics: Success");
    }

    private void HandleStatisticsUpdateFailure(GameEventType gameEventType, object[] data)
    {
        if (gameEventType != GameEventType.UserStatisticsUpdateFailed)
        {
            return;
        }

        print($"Update statistics: Failed\n{(string)data[0]}");
    }

    private void UpdateQueuedScoreOnRegistrationSucceed(GameEventType gameEventType)
    {
        if (gameEventType != GameEventType.UserRegistrationSucceed)
        {
            return;
        }

        RequestStatisticsUpdate();
    }

    private void UpdateQueuedScoreOnLoginSucceed(GameEventType gameEventType)
    {
        if (gameEventType != GameEventType.UserLoginSucceed)
        {
            return;
        }

        RequestStatisticsUpdate();
    }

    private void RequestStatisticsUpdate()
    {
        if (_score == null)
        {
            return;
        }

        _playfabStatsUpdater = new PlayfabStatsUpdater(score: _score.Value);
        _score = null;
    }

    private void OnLoginButtonClick()
    {
        SetScreenActiveAndUpdateMessage();
        GameEventHandler.RaiseEvent(GameEventType.RequestUserAuth);
    }

    private void OnCloseButtonClick()
    {
        SetScreenActiveAndUpdateMessage();
    }

    private void OnDontAskAgainToggleValueChange(bool isOn)
    {
        ProfileData.Manager.SetLoginReminderState(isOn);
    }

    private void SetScreenActiveAndUpdateMessage(bool isActive = false)
    {      
        _buttons[0].Deselect();
        _loginReminderMessage.text = _message;
        GlobalFunctions.Loop<CanvasGroup>.Foreach(_canvasGroups, canvasGroup => SetCanvasGroupActive(canvasGroup, isActive));
    }
}