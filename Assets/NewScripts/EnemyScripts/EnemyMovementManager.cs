using System.Collections;
using UnityEngine;
using Pautik;

public class EnemyMovementManager : MonoBehaviour , IFollowTheTarget, IEnemyMovementManager
{
    private enum MovementState { Normal, Slow }
    private MovementState _movementState;

    [SerializeField] private Rigidbody2D _rigidbody;

    public float speed;

    [SerializeField] Transform direction;

    

    public float zAxis;
    public float lerp;

    private Rigidbody2D _playerRigidbody;



    private Vector2 _movementDirection;

    private float _distanceFromPlayer;
    private float lookAtAngle;


    private Animator anim;

    public bool HasFrontCollision { get; set; }
    public bool HasTopCollision { get; set; }
    public bool HasBottomCollision { get; set; }
    public bool CanChaseTarget { get; set; } = true;

    private void Awake() 
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        anim = transform.GetChild(0).GetComponent<Animator>();

        GetPlayerRigidbody();
    }

    private void GetPlayerRigidbody()
    {
        _playerRigidbody = Get<Rigidbody2D>.From(FindObjectOfType<MovementController>().gameObject);
    }

    private void Start() 
    {
       StartCoroutine(Turbulence(Random.Range(0, 1), Random.Range(1, 3), Random.Range(-0.1f, 0.1f)));
    }

    private void AssignMovementDirection()
    {
        _movementDirection = _playerRigidbody.position - _rigidbody.position;
    }

    private void GetDistanceFromPlayer()
    {
        _distanceFromPlayer = Vector2.Distance(_rigidbody.position, _playerRigidbody.position);
    }

    

    private void FixedUpdate() 
    {
        AssignMovementDirection();
        GetDistanceFromPlayer();
        
        

        zAxis = transform.eulerAngles.z;

        CheckCurrentState();
        AnimationController();
        IncreaseTargetFollowForce();

        if (HasFrontCollision) {
            print("HasFrontCollision");
            _movementState = MovementState.Slow;
            StartCoroutine(AvoidCollisionRotation(10, -2));

        }
        if (HasTopCollision) {
            print("HasTopCollision");

            StartCoroutine(AvoidCollisionRotation(10, -2));

        }
        if (HasBottomCollision) {
            print("HasBottomCollision");
            StartCoroutine(AvoidCollisionRotation(10, 2));

        }

        if (!HasFrontCollision && !HasTopCollision && !HasBottomCollision && CanChaseTarget) {

            print("Normal");
            _movementState = MovementState.Normal;

            if (_distanceFromPlayer <= 50) {

                IFollowTheTarget I_follow = GetComponent<IFollowTheTarget>();

                if (I_follow != null) {

                    I_follow.FollowTarget();
                }
            }
        }
    }
  
    private void CheckCurrentState() {

        switch (_movementState) {

            case MovementState.Normal: Movement(110);break;
            case MovementState.Slow: Movement(100); break;         
        }
    }

    private void Movement(float speed) {

        this.speed = speed;
        _rigidbody.velocity = (direction.position - transform.position) * speed * Time.deltaTime;
    }

    private IEnumerator AvoidCollisionRotation(float time,float angle) 
    {
            while (time > 0) {

                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, zAxis + angle), 5 * Time.deltaTime);
                time--;

                yield return null;
        }

    }

    public void FollowTarget() {

        lookAtAngle = Mathf.Atan2(_movementDirection.y, _movementDirection.x) * Mathf.Rad2Deg;

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

        if(zAxis < 225 && zAxis > 135) {

            tempScale_y = -1;
        }

        transform.localScale = new Vector3(1, tempScale_y, 1);
    }

    private IEnumerator Turbulence(float time, float delay, float point) {

        yield return new WaitForSeconds(time);

        while (delay > 0) {

            if (CanChaseTarget) {

                delay -= Time.deltaTime;
                float y = transform.position.y;
                y += point * Time.deltaTime;
                transform.position = new Vector2(transform.position.x, y);
            }

            yield return null;

        }
    }

    private void IncreaseTargetFollowForce() 
    {
        if(_distanceFromPlayer <= 10) {

            lerp = 0.3f + Reference.Manager.EnemySpawner.EnemyRotationLerp;
        }
        
        else if(_distanceFromPlayer <= 100 && _distanceFromPlayer > 10) {

            lerp = 2 + Reference.Manager.EnemySpawner.EnemyRotationLerp;
        }     
    }
} 