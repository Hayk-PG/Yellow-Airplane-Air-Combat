using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BestScoreControllerScript : MonoBehaviour
{

    //public static BestScoreControllerScript instance;

    public int scorePoint;


    private Text UIbestScore;
    public int UIbestScorepoint = 0;


    private void Awake() {
/*
        if (instance == null) {

            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != null) {

            Destroy(gameObject);
        }
*/


        scorePoint = GameObject.Find("ScoreController").GetComponent<ScoreScript>().scorePoint;
        UIbestScore = GameObject.Find("BestScoreText").GetComponent<Text>();

        UIbestScore.text = PlayerPrefs.GetInt("UIbestScorepoint", UIbestScorepoint).ToString();

    }


    private void Start() {

        

       


    }

    private void Update() {
        
        if(scorePoint > UIbestScorepoint) {

            UIbestScorepoint = scorePoint;
            PlayerPrefs.SetInt("UIbestScorepoint", UIbestScorepoint);
        }


        PlayerPrefs.Save();

        print(UIbestScorepoint);

    }




}
