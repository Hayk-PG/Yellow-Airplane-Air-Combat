using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static SoundManagerScript instance;

    private AudioSource src;
    private AudioClip clickSound , heartSound , hit;


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

        src = GetComponent<AudioSource>();
        clickSound = Resources.Load("SoundFX/Click Sound") as AudioClip;
        heartSound = Resources.Load("SoundFX/HeartSound2") as AudioClip;
        hit = Resources.Load("SoundFX/HitSound1") as AudioClip;

    }


    public void ChangeSoundFX(string soundName) {

        switch (soundName) {

            case "clickSound": src.PlayOneShot(clickSound); src.volume = 1; break;
            case "heartSound": src.PlayOneShot(heartSound); src.volume = 0.3f; break;
            case "hit": src.PlayOneShot(hit); src.volume = 1; break;
        }

    }



}
