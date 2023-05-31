using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    private GameObject player;
    public GameObject UI , GameOverScreen , PauseButton;

    private GameObject[] engineSounds;

    public GameObject leaderBoard_button;

    public ScoreScript scoreScript;

    public int num;

    private void Update() {

        engineSounds = GameObject.FindGameObjectsWithTag("EngineSound");

        player = GameObject.FindGameObjectWithTag("Player");

        if(player == null) {

            Invoke("GameOver", 3);
            UI.gameObject.SetActive(false);
            PauseButton.SetActive(false);

            
        }
        else if(player != null) {

            UI.gameObject.SetActive(true);
            GameOverScreen.gameObject.SetActive(false);
        }
    }

    private void GameOver() {

        GameOverScreen.gameObject.SetActive(true);

        leaderBoard_button.SetActive(true);
        leaderBoard_button.transform.parent = GameOverScreen.transform;

        for (int i = 0; i < engineSounds.Length; i++) {

            engineSounds[i].SetActive(false);
        }

        Time.timeScale = 0;
        BackGroundMusicScript.instance.PlayMusic();

        //PlayGameServicesScript.instance.AddScoreToLeaderboard(scoreScript.UIbestScorepoint);


    }

    public void PlayAgain() {

        SoundManagerScript.instance.ChangeSoundFX("clickSound");
        BackGroundMusicScript.instance.StopMusic();
        SceneManager.LoadScene("Camel");
        Time.timeScale = 1;

        
        ShowAdsScript.instance.num++;
        this.num = ShowAdsScript.instance.num;
        print(num);

        if (ShowAdsScript.instance.showBannerAd) {

            AdMobScipt.instance.ShowBannerAd();
            print("BANNER AD IS TRUE");
        }

      
    }

    public void QuitTheGame() {

        SoundManagerScript.instance.ChangeSoundFX("clickSound");
        Application.Quit();
    }




}
