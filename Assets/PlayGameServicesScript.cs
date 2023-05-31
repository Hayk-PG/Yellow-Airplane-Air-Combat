//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using GooglePlayGames;
//using GooglePlayGames.BasicApi;
//using UnityEngine.UI;


//public class PlayGameServicesScript : MonoBehaviour
//{
//    public static PlayGameServicesScript instance;

//    private void Awake() {
        
//        if(instance != null) {

//            Destroy(gameObject);
//        }
//        else {
//            instance = this;
//            DontDestroyOnLoad(gameObject);
//        }


//    }
//    void Start() {
//        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
//        PlayGamesPlatform.InitializeInstance(config);
//        PlayGamesPlatform.Activate();

//        SignIn();
//    }


//    void SignIn() {
//        Social.localUser.Authenticate(success => {});
//    }


//    public void AddScoreToLeaderboard(long score) {
       
//        Social.ReportScore(score, GPGSIds.leaderboard_best, success => {});
//    }

//    public void ShowLeaderboardsUI() {
//        //PlayGamesPlatform.Instance.ShowLeaderboardUI(GPGSIds.leaderboard_best);
//        Social.ShowLeaderboardUI();
//    }

  
//}
