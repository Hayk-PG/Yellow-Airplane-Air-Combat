using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoverPhotoScript : MonoBehaviour
{

    private void Start() {

        //Invoke("LoadGame", 3);
    }

    private void LoadGame() {

        SceneManager.LoadScene("Camel");
    }
}
