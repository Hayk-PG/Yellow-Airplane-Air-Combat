using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireScript : MonoBehaviour
{
    private float x;
    public Transform player;

  
    private void Update() {

        x = transform.localScale.x;

        if (player.transform.localScale.x > 0) {

            x = 1;
        }
        else
            x = -1;

        transform.localScale = new Vector3(x, 1, 1);
    }
}
