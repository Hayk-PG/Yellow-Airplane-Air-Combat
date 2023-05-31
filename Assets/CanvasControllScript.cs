using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasControllScript : MonoBehaviour
{

    public static CanvasControllScript instance;
    void Start()
    {

        if (instance != null) {

            Destroy(gameObject);
        }
        else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

   
}
