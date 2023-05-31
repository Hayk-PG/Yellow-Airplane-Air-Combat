using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
   
    private Text scoreText;
    public int scorePoint;

    private Text UIbestScore;
    public int UIbestScorepoint = 0;

    public Text MenuBestScore;

    private Text GameOverBestScore;
    private Text GameOverScore;
    private int gameOverScorePoint;

    public Text pauseScreenScoreText;

    private void OnEnable() {

        scorePoint = 0;
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        UIbestScore = GameObject.Find("BestScoreText").GetComponent<Text>();

       

        UIbestScorepoint = PlayerPrefs.GetInt(" UIbestScorepoint", UIbestScorepoint);
        UIbestScore.text = UIbestScorepoint.ToString();

        MenuBestScore.text = UIbestScorepoint.ToString();


      
    }
  
   
    private void Update() {

   
        scoreText.text = scorePoint.ToString();

        if(scorePoint > UIbestScorepoint) {

            UIbestScorepoint = scorePoint;
        }

        UIbestScoreController();
        GameOverScoreText();

        PauseScreenScore();
     





        PlayerPrefs.Save();

    }

   

    private void UIbestScoreController() {

        PlayerPrefs.SetInt(" UIbestScorepoint", UIbestScorepoint);
    }

    private void GameOverScoreText() {

        if(GameObject.Find("BestScoreText_GameOver") != null) {

            GameOverBestScore = GameObject.Find("BestScoreText_GameOver").GetComponent<Text>();
            GameOverBestScore.text = UIbestScorepoint.ToString();
        }
       
        if (GameObject.Find("ScoreText_GameOver") != null) {

            GameOverScore = GameObject.Find("ScoreText_GameOver").GetComponent<Text>();
            PlayerPrefs.SetInt(" scorePoint", scorePoint);
            gameOverScorePoint = PlayerPrefs.GetInt(" scorePoint", scorePoint);
            GameOverScore.text = gameOverScorePoint.ToString();
        }
       


       
       


    }


    public void PauseScreenScore() {
        pauseScreenScoreText.text = scorePoint.ToString();

    }

    /*public void LeaderBoardScore() {

        PlayGameServicesScript.instance.AddScoreToLeaderboard(UIbestScorepoint);
       
    }*/

} // CLASS

