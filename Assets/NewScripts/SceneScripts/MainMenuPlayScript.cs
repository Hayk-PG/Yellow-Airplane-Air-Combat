using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuPlayScript : MonoBehaviour
{
    public GameObject text;
    public GameObject buttons;

    private Animator anim;

    private AudioSource src;
    private AudioClip click;

    private void OnEnable() {

        anim = text.GetComponent<Animator>();
        TextAnimation();

        buttons.SetActive(false);
    }

    private void Start() {

        src = GetComponent<AudioSource>();
        click = Resources.Load("SoundFX/Click Sound") as AudioClip;


        TextAnimation();
    }

    private void Update() {

        ButtonSetToActive();

    }



    public void StartTheGame() {

        src.PlayOneShot(click);
        SceneManager.LoadScene("Camel");
    }

    public void QuitTheGame() {

        src.PlayOneShot(click);
        Application.Quit();
    }



    public void TextAnimation() {

        anim.Play("StartSceneTextAnimation",0);

       
    }


    private void ButtonSetToActive() {

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("StartSceneTextAnimation") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1) {

            buttons.SetActive(true);
        }
    }
}
