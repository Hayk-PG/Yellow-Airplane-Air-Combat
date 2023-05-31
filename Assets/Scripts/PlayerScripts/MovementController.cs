using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MovementController : MonoBehaviour
{
    public float speed = 80;
    public float lerp;
    private float angle;
    public float zAxis;

    public int angleExtraValue;

    private Vector3 rotateDirection;
    public Vector3 touchPosition;


    private Rigidbody2D rb;
   
    private Transform movementDirection;

    private Quaternion rotation;

    public bool outOfControll;

    public AudioSource audioSrc;

    private FixedJoystick joystick;

    private Animator anim;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    private void Start() {

        StartCoroutine(LowSpeedRotation());

        joystick = GameObject.FindGameObjectWithTag("Joystick").GetComponent<FixedJoystick>();
    }



    private void Update() {

        zAxis = transform.eulerAngles.z;

        ControllingTheSpeed();
/*
        IsPointerOverUIObject();*/

        audioSrc.pitch = speed / 100;

     
    }





    private void FixedUpdate() {

        //FlippX();
        AnimationController();

        if (!outOfControll) {

            TouchRotation();
            MoveHorizontal();
        }
    }





    private void MoveHorizontal() {

        movementDirection = transform.GetChild(2).transform;
        rb.velocity = (movementDirection.position - transform.position) * speed * Time.deltaTime;
    }

    private void TouchRotation() {

      /*  if (!IsPointerOverUIObject()) {

            for (int i = 0; i < Input.touchCount; i++) {

                touchPosition = Camera.main.ScreenToWorldPoint(Input.touches[i].position);
              
            }
        }

       
        touchPosition.z = 0;*/




/*
        rotateDirection = touchPosition - transform.position;
        angle = Mathf.Atan2(rotateDirection.y, rotateDirection.x) * Mathf.Rad2Deg;
     

        if (Input.touchCount > 0 && touchPosition.x > transform.position.x && transform.localScale.x > 0) {

            rotation = Quaternion.Euler(0, 0, angle);

        }

        if (Input.touchCount > 0 && touchPosition.x < transform.position.x && transform.localScale.x < 0) {

            rotation = Quaternion.Euler(0, 0, angle + angleExtraValue);

        }
            
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, lerp * Time.deltaTime);
*/



        angle = Mathf.Atan2(joystick.Vertical, joystick.Horizontal) * Mathf.Rad2Deg;
        Quaternion r = Quaternion.Euler(0, 0, angle);

        if(joystick.Horizontal != 0 || joystick.Vertical != 0) {

            transform.rotation = Quaternion.Slerp(transform.rotation, r, 5 * Time.deltaTime);

        }


    }


   /* private void FlippX() {

        if (Input.touchCount > 0 && touchPosition.x < transform.position.x) {

            transform.localScale = new Vector3(-1, 1, 1);

        }
        else if (Input.touchCount > 0 && touchPosition.x > transform.position.x) {

            transform.localScale = new Vector3(1, 1, 1);
        }

        }*/

   

    private void ControllingTheSpeed() {

        float x = transform.localScale.x;

        //------ INCREASE SPEED WHEN THE SPEED IS BELOW 80---------
        if(x > 0 && zAxis >= 30 && zAxis <= 90 && speed <= 80 ) {

            speed -= 0.01f * Time.deltaTime;
        }

        if (x < 0 && zAxis <= 330 && zAxis >= 270 && speed <= 80) {

            speed -= 0.01f * Time.deltaTime;
        }
        //---------------------------------------------------------

        //------ INCREASE SPEED WHEN THE SPEED IS <= 100 && > 80---------
        if (x > 0 && zAxis >= 30 && zAxis <= 90 && speed > 80) {

            speed -= 5 * Time.deltaTime;
        }

        if (x < 0 && zAxis <= 330 && zAxis >= 270 && speed > 80) {

            speed -= 5 * Time.deltaTime;
        }
        //---------------------------------------------------------


        if (x > 0 && zAxis <= 330 && zAxis >= 270) {

            speed += 5 * Time.deltaTime;
        }

        

        if (x < 0 && zAxis >= 30 && zAxis <= 90) {

            speed += 5 * Time.deltaTime;
        }

      

        if (speed <= 30) {

            speed = 30;
            outOfControll = true;
        }

        if(speed >= 100) {

            speed = 100;
        }
        
    }

    
    private IEnumerator LowSpeedRotation() {

       

        if (outOfControll) {

            StartCoroutine(R());

            yield return  StartCoroutine(R());

            outOfControll = false;

        }

        yield return null;

        StartCoroutine(LowSpeedRotation());
    }


    private IEnumerator R() {

        float duration = 3;
        float z = transform.eulerAngles.z;

        while (duration > 0) {

            z -= 40 * Time.deltaTime;
            transform.rotation = Quaternion.Euler(0, 0, z);
            duration -= Time.deltaTime;

            yield return null;
        }
        
    }


    // CHECKING IF TOUCHPOSITION EQAULS GUI BUTTON POSITION OR NOT
    /*private bool IsPointerOverUIObject() {

        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;

    }
*/



    private void AnimationController() {

        float tempScale_y = transform.localScale.y;

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Neutral") && zAxis > 45 && anim.GetCurrentAnimatorStateInfo(0).IsName("Neutral") && zAxis < 135 || anim.GetCurrentAnimatorStateInfo(0).IsName("Neutral") && zAxis > 225 && anim.GetCurrentAnimatorStateInfo(0).IsName("Neutral") && zAxis < 315) {

            anim.SetTrigger("up");
            tempScale_y = 1;
        }



        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Upwards") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1) {

            if (zAxis < 45 || zAxis < 225 && zAxis > 135) {

                anim.SetTrigger("side");
            }
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Sidewards") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1) {

            anim.SetTrigger("i");
        }


        // FLIPP SCALE Y AXIS

        if (zAxis < 225 && zAxis > 135) {

            tempScale_y = -1;
        }

        transform.localScale = new Vector3(1, tempScale_y, 1);

        //------------------------------------------



    }





}// CLASS






