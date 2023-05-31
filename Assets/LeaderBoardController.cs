using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderBoardController : MonoBehaviour
{
    public long num; 
   public void ShowLeaderBoard() {

        //PlayGameServicesScript.instance.ShowLeaderboardsUI();
    }

    public void Increment() {

        num += 10;
        print(num);
        
    }

    public void PostToLeaderBoard() {

        //PlayGameServicesScript.instance.AddScoreToLeaderboard(num);
    }
}
