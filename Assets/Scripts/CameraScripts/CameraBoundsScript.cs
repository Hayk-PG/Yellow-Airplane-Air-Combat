using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBoundsScript : MonoBehaviour
{
    private BoxCollider2D camBox;
    private Camera cam;

 
    private float height, width;
    private void Awake() {

        cam = Camera.main;
        camBox = GetComponent<BoxCollider2D>();

    }
    private void Update() {

        height = cam.orthographicSize * 2;
        width = height * cam.aspect;

        camBox.size = new Vector2(width, height);
        
    }





}//CLASS
