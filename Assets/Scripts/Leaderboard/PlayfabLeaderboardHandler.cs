using PlayFab;
using PlayFab.ClientModels;

public class PlayfabLeaderboardHandler : BasePlayfabHandler<GetLeaderboardRequest, GetLeaderboardResult>
{
    public PlayfabLeaderboardHandler()
    {
        _request = new GetLeaderboardRequest { MaxResultsCount = 10, StartPosition = 0, StatisticName = _leaderboardName };

        PlayFabClientAPI.GetLeaderboard(_request, OnSucceed, OnFailed);
        GameEventHandler.RaiseEvent(GameEventType.LeaderboardProceed);
    }

    protected override void OnSucceed(GetLeaderboardResult result)
    {
        _data[0] = result;

        GameEventHandler.RaiseEvent(GameEventType.LeaderboardSucceed, _data);
    }

    protected override void OnFailed(PlayFabError error)
    {
        _data[0] = error.ErrorMessage;

        GameEventHandler.RaiseEvent(GameEventType.LeaderboardFailed, _data);
    }
}