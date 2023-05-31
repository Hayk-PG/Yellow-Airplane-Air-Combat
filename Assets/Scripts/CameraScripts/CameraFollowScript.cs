using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    public Transform player;
    private BoxCollider2D box;

    private float desiredY;
    private float currentY;
    private float desiredX;
    private float currentX;

    private void Awake() {

        box = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate() {

        currentY = transform.position.y;
        currentX = transform.position.x;

        // Y AXIS

        if(player.position.y > box.bounds.center.y  && currentY <= 43) {

            desiredY = Mathf.Lerp(currentY, player.position.y, 10 * Time.deltaTime);
        }

        else if (player.position.y < box.bounds.center.y  && currentY >= -43) {

            desiredY = Mathf.Lerp(currentY, player.position.y, 10 * Time.deltaTime);
        }

        if(currentY >= 43) {

            currentY = 43;
        }

        else if(currentY <= -43) {

            currentY = -43;
        }

        // X AXIS

        if (player.position.x > box.bounds.center.x && currentX <= 40) {

            desiredX = Mathf.Lerp(currentX, player.position.x, 10 * Time.deltaTime);
        }

        else if (player.position.x < box.bounds.center.x && currentX >= -40) {

            desiredX = Mathf.Lerp(currentX, player.position.x, 10 * Time.deltaTime);
        }

        if (currentX >= 40) {

            currentX = 40;
        }

        else if (currentX <= -40) {

            currentX = -40;
        }

        transform.position = new Vector3(desiredX, desiredY, -10);
    }


} // CLASS
