using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMusicScript : MonoBehaviour
{
    public static BackGroundMusicScript instance;

    private AudioSource src;
    private AudioClip music;




    private void Awake() {
        
        if(instance != null) {

            Destroy(gameObject);
        }

        else {

            instance = this;
            DontDestroyOnLoad(gameObject);
        }

    }


    private void Start() {

        music = Resources.Load("BackGroundMusic 2") as AudioClip;
        src = GetComponent<AudioSource>();

        PlayMusic();
    }

    private void Update() {

        
    }
    public void PlayMusic() {
        src.Play();
    }


    public void StopMusic() {

        src.Pause();
    }

}
