using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class PlayfabLeaderboard : MonoBehaviour
{
    private void Test()
    {
        GetLeaderboardRequest getLeaderboardRequest = new GetLeaderboardRequest();        
        getLeaderboardRequest.MaxResultsCount = 10;
        getLeaderboardRequest.StartPosition = 0;
        getLeaderboardRequest.StatisticName = "Leaderboard";
        PlayFabClientAPI.GetLeaderboard(getLeaderboardRequest, OnSuccess, OnError);
    }

    private void OnSuccess(GetLeaderboardResult getLeaderboardResult)
    {

    }

    private void OnError(PlayFabError playFabError)
    {

    }
}
