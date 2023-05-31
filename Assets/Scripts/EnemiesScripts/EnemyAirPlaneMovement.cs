using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAirPlaneMovement : MonoBehaviour
{
   
    public float speed;
    private float magnitude;
    public float magnitudeDistance;
    private float angle;

    private bool canRotate;

    private Rigidbody2D rb;
    private EnemiesController controller;

    private Transform player;
    public Transform movementDirection;

    private Quaternion rotation;

    private Vector3 distance;



    private void Awake() {

        controller = GameObject.Find("EnemiesController").GetComponent<EnemiesController>();

        rb = GetComponent<Rigidbody2D>();

    }



    private void Update() {

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        canRotate = GetComponent<EnemyShootScript>().canShoot;

        //-----------------------------------------------

        controller.Speed(this.gameObject.name);
        this.speed = controller.speed;

        //-----------------------------------------------

        distance = player.position - transform.position;
        magnitude = distance.magnitude;
        

        Movement();

        if (magnitude <= magnitudeDistance && canRotate) {

            RotationToPlayer(true);
        }
        else
            RotationToPlayer(false);

    }



    private void Movement() {

        rb.velocity = (movementDirection.position - transform.position) * speed * Time.deltaTime;

    }


    private void RotationToPlayer(bool canFollow) {

        angle = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg;
        rotation = Quaternion.Euler(0, 0, angle);

        if (canFollow) {

            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 5 * Time.deltaTime);
        }

    }



} //ClASS
