using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementController : MonoBehaviour , IFollowTheTarget
{
   //-------------------------------------------

    private enum State { FLY_NORMAL ,FLY_SLOW }
    private State state;

    private Rigidbody2D rb;

    public float speed;

    [SerializeField] Transform direction;

    public bool frontCollisionDetected;
    public bool topCollisionDetected;
    public bool bottomCollisionDetected;

    public float zAxis;
    public float lerp;
    private float extraLerpPoint;
    [SerializeField] float angle;

    //---------------------------------------

    // FOLLOW INTERFACE

    private Transform target;

    private Vector3 distance;

    [SerializeField] private float magnitude;
    private float lookAtAngle;

    public bool tooMuchHitInFront;
    public bool playerChasing;

    // ANIMATION

    private Animator anim;





    private void Awake() {

        rb = GetComponent<Rigidbody2D>();
        anim = transform.GetChild(0).GetComponent<Animator>();

    }




    private void Start() {

        //TURBULENCE
       StartCoroutine(Turbulence(Random.Range(0, 1), Random.Range(1, 3), Random.Range(-0.1f, 0.1f)));

    }






    private void FixedUpdate() {

        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        distance = target.position - transform.position;
        magnitude = distance.magnitude;

        zAxis = transform.eulerAngles.z;

 
        // STATE SWITCH FUNCTION
        CheckCurrentState();


        // ANIMATION
        AnimationController();

        // FOLLOW ROTATION LERP MULTIPLIER
        TargetFollowForceController();





        // CHECKING IF THIS GAMEOBJECT HITS ANYTHING OR NOT
        // IF HITS ,THEN ITS AVOIDING THE COLLISION GAMEOBJECT AND CANT FOLLOW THE TARGET

        if (frontCollisionDetected) {

            state = State.FLY_SLOW;
            StartCoroutine(AvoidCollisionRotation(10, -2));

        }
        if (topCollisionDetected) {


            StartCoroutine(AvoidCollisionRotation(10, -2));

        }
        if (bottomCollisionDetected) {

            StartCoroutine(AvoidCollisionRotation(10, 2));

        }


        // ------------------------------------------------------

        // MAKING SURE THIS GAMEOBJECT DOESNT HIT ANYTHING

        if (!frontCollisionDetected && !topCollisionDetected && !bottomCollisionDetected && !tooMuchHitInFront && !playerChasing) {

            state = State.FLY_NORMAL;

            if (magnitude <= 50) {

                IFollowTheTarget I_follow = GetComponent<IFollowTheTarget>();

                if (I_follow != null) {

                    I_follow.FollowTarget();
                }
            }
        }

        // ---------------------------------------------------------

    }
  







    private void CheckCurrentState() {

        switch (state) {

            case State.FLY_NORMAL: Movement(110);break;
            case State.FLY_SLOW: Movement(100); break;
           
        }

    }





    private void Movement(float speed) {

        this.speed = speed;
        rb.velocity = (direction.position - transform.position) * speed * Time.deltaTime;


    }



   private IEnumerator AvoidCollisionRotation(float time,float angle) {

        this.angle = angle;

            while (time > 0) {

                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, zAxis + angle), 5 * Time.deltaTime);
                time--;

                yield return null;
            
        }
      
    }




    public void FollowTarget() {

        lookAtAngle = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg;

        Quaternion currentRotation = transform.rotation;
        Quaternion desiredRotation = Quaternion.Euler(0, 0, lookAtAngle);

        transform.rotation = Quaternion.Lerp(currentRotation, desiredRotation, lerp * Time.deltaTime);


    }



    private void AnimationController() {

        float tempScale_y = transform.localScale.y;

        if(anim.GetCurrentAnimatorStateInfo(0).IsName("Neutral") && zAxis > 45 && anim.GetCurrentAnimatorStateInfo(0).IsName("Neutral") && zAxis < 135 || anim.GetCurrentAnimatorStateInfo(0).IsName("Neutral") && zAxis > 225 && anim.GetCurrentAnimatorStateInfo(0).IsName("Neutral") && zAxis < 315) {

            anim.SetTrigger("up");
            tempScale_y = 1;
        }



        if(anim.GetCurrentAnimatorStateInfo(0).IsName("Upwards") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1) {

            if(zAxis < 45 || zAxis < 225 && zAxis > 135) {

                anim.SetTrigger("side");
            }
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Sidewards") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1) {

            anim.SetTrigger("i");
        }


        // FLIPP SCALE Y AXIS

        if(zAxis < 225 && zAxis > 135) {

            tempScale_y = -1;
        }

        transform.localScale = new Vector3(1, tempScale_y, 1);

        //------------------------------------------



    }



    // TURBULENCE

    private IEnumerator Turbulence(float time, float delay, float point) {

        yield return new WaitForSeconds(time);

        while (delay > 0) {

            if (playerChasing) {

                delay -= Time.deltaTime;
                float y = transform.position.y;
                y += point * Time.deltaTime;
                transform.position = new Vector2(transform.position.x, y);
            }


            yield return null;

        }

       
    }


    //--------------------------------------------------------------------------------------------


    // FOLLOW TARGET LERP MULTIPLIER
    private void TargetFollowForceController() {

        extraLerpPoint = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>().enemyRotationLerp;

        if(magnitude <= 10) {

            lerp = 0.3f + extraLerpPoint;
        }
        
        else if(magnitude <= 100 && magnitude > 10) {

            lerp = 2 + extraLerpPoint;
        }
       

      
    }

    //---------------------------------------------------------------------------------------------






} //CLASS
