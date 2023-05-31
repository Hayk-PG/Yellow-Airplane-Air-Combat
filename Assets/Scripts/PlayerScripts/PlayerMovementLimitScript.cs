using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementLimitScript : MonoBehaviour
{
    // CAMERA 
    public float maxY, minY;
    public float maxX, minX;
    //PLAYER
    private float posY;
    private float posX;

    private BoxCollider2D box;

    private void Awake() {

        box = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<BoxCollider2D>();
    }

    private void Update() {

        maxY = box.bounds.center.y + (box.size.y / 2);
        minY = box.bounds.center.y - (box.size.y / 2);
        maxX = box.bounds.center.x + (box.size.x / 2);
        minX = box.bounds.center.x - (box.size.x / 2);

        posX = transform.position.x;
        posY = transform.position.y;

        if(posX >= maxX) {

            posX = maxX;
        }

        else if(posX <= minX) {

            posX = minX;
        }

        if (posY >= maxY) {

            posY = maxY;
        }

        else if (posY <= minY) {

            posY = minY;
        }

        transform.position = new Vector2(posX, posY);

    }




}//CLASS
