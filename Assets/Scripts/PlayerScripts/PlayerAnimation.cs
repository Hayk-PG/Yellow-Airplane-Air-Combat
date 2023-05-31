using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;

    private Vector3 touchPosition;
 
    private void Awake() {

        anim = GetComponentInChildren<Animator>();


    }

    private void Update() {

        touchPosition = GetComponent<MovementController>().touchPosition;

        Animate();

    }

    private void Animate() {

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle")) {

            if(Input.touchCount > 0 && touchPosition.x < transform.position.x ) {

                anim.SetTrigger("left");
            }
        }

        if(anim.GetCurrentAnimatorStateInfo(0).IsName("Left") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1) {

           
            if (Input.touchCount > 0 && touchPosition.x > transform.position.x ) {

                anim.SetTrigger("right");
            }

        }

    }


} //CLASS


