using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBottomSensorScript : MonoBehaviour
{
    bool c;

    private void Update() {

        transform.parent.GetComponent<EnemyMovementController>().bottomCollisionDetected = c;
    }




    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.tag != "MainCamera" && collision.gameObject.tag != "Player" && collision.gameObject.tag != "Enemy" && collision.gameObject.tag != "Bullet" ) {

            c = true;

            //ADDING 1 TO COLLISION'S GAMEOBJECT RELATED "SCRIPT"
            if (collision.gameObject.transform.parent != null) {

                if (collision.gameObject.transform.parent.GetChild(3).GetComponent<EnemyFrontSensorScript>().frontCollisionCounter < 5) {

                     if(collision.gameObject.transform.parent.GetChild(3).GetComponent<EnemyFrontSensorScript>().frontCollisionCounter >= 0) { 
  
                        collision.gameObject.transform.parent.GetChild(3).GetComponent<EnemyFrontSensorScript>().frontCollisionCounter++;
                    }
                }
            }

            else
                return;
            

        }

    
    }


    private void OnTriggerExit2D(Collider2D collision) {

        if (collision.gameObject.tag != "MainCamera" && collision.gameObject.tag != "Player" && collision.gameObject.tag != "Enemy" && collision.gameObject.tag != "Bullet") {

            c = false;
        }

    }









} // CLASS
