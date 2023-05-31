using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFrontSensorScript : MonoBehaviour 
{
    bool c;
    [SerializeField] bool cantFollow;

    public int frontCollisionCounter = 0;

    [SerializeField] private float time = 10;

    private void Update() {

        transform.parent.GetComponent<EnemyMovementController>().frontCollisionDetected = c;
        transform.parent.GetComponent<EnemyMovementController>().tooMuchHitInFront = cantFollow;

        // Checking if this gameObjects PARENT can follow the target or not

        if (frontCollisionCounter > 1) {

            cantFollow = true;
        }
        else
            cantFollow = false;

        // --------------------------------------------------------------

        // After 10 seconds time is equal 0,and then going back to its original value

        time -= 1 * Time.deltaTime;

        if(time <= -1) { time = 10; }


        if (time <= 0) {
            frontCollisionCounter = frontCollisionCounter - frontCollisionCounter; }

        // -----------------------------------------------------------------------------------
        
    }

  


    private void OnTriggerEnter2D(Collider2D collision) {

        if(collision.gameObject.tag != "MainCamera" && collision.gameObject.tag != "Player" && collision.gameObject.tag != "Enemy" && collision.gameObject.tag != "Bullet") {

            c = true;

            frontCollisionCounter++;
        }

       
    }

    private void OnTriggerExit2D(Collider2D collision) {
        
        if (collision.gameObject.tag != "MainCamera" && collision.gameObject.tag != "Player" && collision.gameObject.tag != "Enemy" && collision.gameObject.tag != "Bullet") {

            c = false;
        }

    }



}//CLASS
