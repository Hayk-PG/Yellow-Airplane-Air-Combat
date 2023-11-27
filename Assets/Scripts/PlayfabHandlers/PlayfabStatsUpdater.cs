using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;

public class PlayfabStatsUpdater : BasePlayfabHandler<UpdatePlayerStatisticsRequest, UpdatePlayerStatisticsResult>
{
    public PlayfabStatsUpdater(int score)
    {
        _request = new UpdatePlayerStatisticsRequest();
        _request.Statistics = new List<StatisticUpdate> { new StatisticUpdate { StatisticName = _leaderboardName, Value = score } };

        PlayFabClientAPI.UpdatePlayerStatistics(_request, OnSucceed, OnFailed);
        GameEventHandler.RaiseEvent(GameEventType.UserStatisticsUpdateProceed);
    }

    protected override void OnSucceed(UpdatePlayerStatisticsResult result)
    {
        GameEventHandler.RaiseEvent(GameEventType.UserStatisticsUpdateSucceed);
    }

    protected override void OnFailed(PlayFabError error)
    {
        _data[0] = error.ErrorMessage;
        GameEventHandler.RaiseEvent(GameEventType.UserStatisticsUpdateFailed, _data);
    }
}