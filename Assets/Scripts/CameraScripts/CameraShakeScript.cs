using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeScript : MonoBehaviour
{
    public bool canShake;

    private Animator anim;


    private void Awake() {

        anim = transform.parent.GetComponent<Animator>();
    }

    private void Update() {

      

        if (canShake) {

            anim.SetBool("shake", true);

        }

        if(anim.GetCurrentAnimatorStateInfo(0).IsName("CameraShake") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1) {

            anim.SetBool("shake", false);
            canShake = false;
        }

      



    }





}// CLASS
