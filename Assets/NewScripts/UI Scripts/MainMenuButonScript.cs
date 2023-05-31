using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButonScript : MonoBehaviour
{
    private GameObject HUD;
    private GameObject[] engineSounds;

    private int HUDchildCount;

    private void Update() {

        HUD = GameObject.Find("HUD");
        HUDchildCount = HUD.transform.childCount;
        engineSounds = GameObject.FindGameObjectsWithTag("EngineSound");
        
    }


    public void MainMenuButtonPressed() {

        for (int i = 0; i < HUDchildCount; i++) {

            HUD.transform.GetChild(i).gameObject.SetActive(false);

            this.gameObject.SetActive(false);

            Time.timeScale = 0;

        }

        for (int i = 0; i < engineSounds.Length; i++) {

            engineSounds[i].SetActive(false);
        }

        this.gameObject.transform.parent.GetChild(1).gameObject.SetActive(true);
        this.gameObject.transform.parent.GetChild(2).gameObject.SetActive(true);
        this.gameObject.transform.parent.GetChild(3).gameObject.SetActive(true);
        this.gameObject.transform.parent.GetChild(4).gameObject.SetActive(true);
        this.gameObject.transform.parent.GetChild(5).gameObject.SetActive(true);
        this.gameObject.transform.parent.GetChild(6).gameObject.SetActive(true);

     

        SoundManagerScript.instance.ChangeSoundFX("clickSound");
        BackGroundMusicScript.instance.PlayMusic();
    }


    public void RESUME_Button() {

        for (int i = 0; i < HUDchildCount; i++) {

            if (!HUD.transform.GetChild(i).gameObject.activeSelf) {

                HUD.transform.GetChild(i).gameObject.SetActive(true);

                this.gameObject.SetActive(true);

                Time.timeScale = 1;
            }
        }

        for (int i = 0; i < engineSounds.Length; i++) {

            engineSounds[i].SetActive(true);
        }


        this.gameObject.transform.parent.GetChild(1).gameObject.SetActive(false);
        this.gameObject.transform.parent.GetChild(2).gameObject.SetActive(false);
        this.gameObject.transform.parent.GetChild(3).gameObject.SetActive(false);
        this.gameObject.transform.parent.GetChild(4).gameObject.SetActive(false);
        this.gameObject.transform.parent.GetChild(5).gameObject.SetActive(false);
        this.gameObject.transform.parent.GetChild(6).gameObject.SetActive(false);



        SoundManagerScript.instance.ChangeSoundFX("clickSound");
        BackGroundMusicScript.instance.StopMusic();

    }


    public void QUIT_Button() {

        SoundManagerScript.instance.ChangeSoundFX("clickSound");
        Application.Quit();

    }






}
