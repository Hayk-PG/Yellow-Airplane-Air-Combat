using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrailScript : MonoBehaviour
{
    private float x;
    private Transform trail;
   
    private void Awake() {

        trail = transform.GetChild(1).transform;
    }

    private void Update() {

        x = trail.localScale.x;
 
        if (transform.localScale.x > 0) {

            x = 1;
        }
        else
            x = -1;

        trail.localScale = new Vector3(x, 1, 1);

    }




} // CLASS
