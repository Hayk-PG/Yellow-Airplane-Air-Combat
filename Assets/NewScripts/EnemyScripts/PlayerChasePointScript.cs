using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChasePointScript : MonoBehaviour
{
    [SerializeField] private bool playerChasing;
   




    private void Update() {

        /*transform.parent.GetComponent<EnemyMovementController>().playerChasing = this.playerChasing;

        if (this.playerChasing) {

            GameObject.FindGameObjectWithTag("Player").GetComponent<MovementController>().speed = 20;
        }
        else if (!this.playerChasing) {

            GameObject.FindGameObjectWithTag("Player").GetComponent<MovementController>().speed = 30;
        }*/
            

    }






    private void OnTriggerEnter2D(Collider2D collision) {

        if(collision.gameObject.tag == "Player") {

            playerChasing = true;
        }
        

    }


    private void OnTriggerExit2D(Collider2D collision) {

        if (collision.gameObject.tag == "Player") {

            playerChasing = false;
        }

    }







} // CLASS
