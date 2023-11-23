using UnityEngine;

public class LeaderboardManager : BaseUserManager
{
    [Header("User Button")]
    [SerializeField] private Btn _leaderboardButton;



    protected override void OnEnable()
    {
        _leaderboardButton.OnSelect += OnLeaderboardButtonClick;
    }

    protected override void OnDisable()
    {
        
    }

    private void OnLeaderboardButtonClick()
    {
        if (!PlayfabUserLoginVerifier.IsLoggedIn)
        {
            GameEventHandler.RaiseEvent(GameEventType.RequestUserAuth);
            return;
        }
    }
}