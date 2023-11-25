using System.Collections;
using UnityEngine;
using Pautik;

public class AIMovementManager : BaseAirplaneMovementManager, IAIMovementManager
{
    private IEnumerator _avoidCollisionCoroutine;   
    private Vector2 _lookDirection;

    private float _distanceFromPlayer;

    public bool CanChaseTarget { get; set; }
    public Vector2 CurrentPosition { get; set; }




    private void OnEnable()
    {
        GameEventHandler.OnEvent += OnGameEvent;
    }

    private void OnDisable()
    {
        GameEventHandler.OnEvent -= OnGameEvent;
    }

    private void OnGameEvent(GameEventType gameEventType, object[] data)
    {
        if (gameEventType != GameEventType.PlayerMoveBroadcast)
        {
            return;
        }

        SetMovementDirection((Vector2)data[0]);
        GetDistanceFromPlayer((Vector2)data[0]);
        TrackCurrentPosition();
        Move();
        Rotate();
        CheckOutOfBounds();
    }

    /// <summary>
    /// Sets the movement direction based on the player's position.
    /// </summary>
    private void SetMovementDirection(Vector2 playerAirplanePosition)
    {
        _lookDirection = playerAirplanePosition - _rigidbody.position;
    }

    /// <summary>
    /// Calculates the distance between the AI and the player.
    /// </summary>
    private void GetDistanceFromPlayer(Vector2 playerAirplanePosition)
    {
        _distanceFromPlayer = Vector2.Distance(_rigidbody.position, playerAirplanePosition);
    }

    /// <summary>
    /// Tracks the current position of the AI.
    /// </summary>
    private void TrackCurrentPosition()
    {
        CurrentPosition = _rigidbody.position;
    }

    /// <summary>
    /// Moves the AI based on the movement direction and speed.
    /// </summary>
    private void Move()
    {
        Move(velocity: Velocity(transform.right, _speed));
    }

    /// <summary>
    /// Rotates the AI towards the player if within a certain distance.
    /// </summary>
    private void Rotate()
    {
        bool canChasePlayer = CanChaseTarget && _distanceFromPlayer < 50f;

        if (!canChasePlayer)
        {
            return;
        }

        Rotate(angle: Angle(_lookDirection));
        UpdateRotationDirection();
    }

    public void DetectFronCollision(Vector2 collidedObjectPosition)
    {
        OnCollision(collidedObjectPosition);
    }

    public void DetectTopCollision(Vector2 collidedObjectPosition)
    {
        OnCollision(collidedObjectPosition);
    }

    public void DetectBottomCollision(Vector2 collidedObjectPosition)
    {
        OnCollision(collidedObjectPosition);
    }

    /// <summary>
    /// Handles collision with another object.
    /// </summary>
    /// <param name="collidedObjectPosition">The position of the collided object.</param>
    private void OnCollision(Vector2 collidedObjectPosition)
    {       
        if(_avoidCollisionCoroutine == null)
        {
            _avoidCollisionCoroutine = AvoidCollision(elapsedTime: 4f, collidedObjectPosition);
            StartCoroutine(_avoidCollisionCoroutine);
        }  
    }

    /// <summary>
    /// Coroutine that handles avoiding collision with another object.
    /// </summary>
    /// <param name="elapsedTime">The duration of the avoidance maneuver.</param>
    /// <param name="collidedObjectPosition">The position of the collided object.</param>
    private IEnumerator AvoidCollision(float elapsedTime, Vector2 collidedObjectPosition)
    {       
        float time = 0;
        float currentZ = transform.rotation.eulerAngles.z;
        float rotationAngle = Vector2.SignedAngle(_rigidbody.position, collidedObjectPosition - _rigidbody.position);
        CanChaseTarget = false;

        while (time < elapsedTime)
        {          
            Rotate(currentZ + rotationAngle);
            time += Time.deltaTime;
            yield return null;
        }

        CanChaseTarget = true;
        _avoidCollisionCoroutine = null;
    }

    /// <summary>
    /// Checks if the object is within the bounds of the map and sets the CanChaseTarget flag accordingly.
    /// </summary>
    private void CheckOutOfBounds()
    {
        bool isWithinHorizontalBounds = _rigidbody.position.x >= Reference.Manager.MapBounds.Min.x && _rigidbody.position.x < Reference.Manager.MapBounds.Max.x;
        bool isWithinVerticalBounds = _rigidbody.position.y >= Reference.Manager.MapBounds.Min.y && _rigidbody.position.y < Reference.Manager.MapBounds.Max.y;

        if(!isWithinHorizontalBounds || !isWithinVerticalBounds)
        {
            CanChaseTarget = true;
        }
    }

    protected override void SetAnimationState(AnimationState animationState)
    {
        
    }

    protected override void UpdateCameraSize(bool reset = false)
    {
        
    }

    protected override void ControlEngineSound()
    {
        
    }
} 