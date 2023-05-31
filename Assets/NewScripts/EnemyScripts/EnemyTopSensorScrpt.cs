using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTopSensorScrpt : MonoBehaviour
{
    bool c;


    private void Update() {

        transform.parent.GetComponent<EnemyMovementController>().topCollisionDetected = c;
    }




    private void OnTriggerEnter2D(Collider2D collision) {


        if (collision.gameObject.tag != "MainCamera" && collision.gameObject.tag != "Player" && collision.gameObject.tag != "Enemy" && collision.gameObject.tag != "Bullet") {

            c = true;
        }

    }




    private void OnTriggerExit2D(Collider2D collision) {


        if (collision.gameObject.tag != "MainCamera" && collision.gameObject.tag != "Player" && collision.gameObject.tag != "Enemy" && collision.gameObject.tag != "Bullet") {

            c = false;
        }



           
    }










}//CLASS
