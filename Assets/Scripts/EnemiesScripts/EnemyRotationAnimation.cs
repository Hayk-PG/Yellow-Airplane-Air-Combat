using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRotationAnimation : MonoBehaviour
{
    public bool canRotate = true;

    private Animator anim;

    public float zAxis;
    public float animZ;

    private Vector3 tempScale;

    private void Awake() {

        anim = transform.GetChild(0).GetComponent<Animator>();
    }

    private void Update() {

        RotationAnim();
    }
    private void RotationAnim() {

        zAxis = transform.eulerAngles.z;
        tempScale = transform.localScale;

        // FLIPP Y AXIS

        if (zAxis > 90 && zAxis < 270) {

            tempScale.y = -1;
        
        }
        else if(zAxis > 270 || zAxis >= 0 && zAxis < 90) {

            tempScale.y = 1;
        }

       

        transform.localScale = tempScale;

        // ANIMATION
        animZ = tempScale.y;

      if(anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") && animZ == -1) {

            anim.SetTrigger("rotate");
        }

     if(anim.GetCurrentAnimatorStateInfo(0).IsName("EnemyRotation") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1) {

            animZ = 0;
        }

     if(tempScale.y > 0 && anim.GetCurrentAnimatorStateInfo(0).IsName("EnemyRotation")) {

            anim.SetTrigger("secondRotate");
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("EnemySecondRotation") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1) {

            anim.SetTrigger("idle");
            animZ = tempScale.y;
        }


    }




}// CLASS
