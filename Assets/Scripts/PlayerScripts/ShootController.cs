using System.Collections;
using UnityEngine;
using Pautik;

public class ShootController : MonoBehaviour
{
    [Header("Shakeable Particle Systems")]
    [SerializeField] private ShakeableParticleSystems _muzzleFlash; 

    [Header("Target Layer Mask")]
    [SerializeField] private LayerMask _targetLayerMask;
   
    private RaycastHit2D[] _raycastHits;
    private RaycastHit2D _hit;
    private ContactFilter2D _contactFilter;
    private Collider2D _targetCollider;
    private IDamage _targetDamage;
    private IEnumerator _fireRoutine;

    private float _fireRate = 650f; 
    private bool _isShooting; 

    


    private void Awake()
    {
        InitializeRaycastParameters();
    }

    private void OnEnable()
    {
        Reference.Manager.InputController.OnInputController += OnInputController;
    }

    private void Update()
    {
        DetectTargetCollider();
    }

    private void OnInputController(InputController.InputType inputType, object[] data)
    {
        HandleShootInput(inputType, data);
    }

    /// <summary>
    /// Handles the shoot input based on the input type.
    /// </summary>
    /// <param name="inputType">The input type.</param>
    /// <param name="data">Additional data associated with the input.</param>
    private void HandleShootInput(InputController.InputType inputType, object[] data) 
    {
        if(inputType != InputController.InputType.PressShootButton)
        {
            return;
        }

        _isShooting = (bool)data[1];
        TryRunCoroutine();
    }

    /// <summary>
    /// Tries to start the shooting coroutine.
    /// </summary>
    private void TryRunCoroutine()
    {
        if (_fireRoutine == null)
        {
            _fireRoutine = FireRoutine(_isShooting);
            StartCoroutine(_fireRoutine);
        }
    }

    /// <summary>
    /// Coroutine for continuous shooting.
    /// </summary>
    private IEnumerator FireRoutine(bool isShooting)
    {
        float elapsedTime = 60f / _fireRate;

        while (_isShooting)
        {
            Shoot();
            ToggleMuzzleFlash();
            PlaySoundEffect();           
            yield return new WaitForSeconds(elapsedTime);
        }

        _fireRoutine = null;
    }

    /// <summary>
    /// Performs shooting at the target.
    /// </summary>
    private void Shoot()
    {
        if (_targetCollider == null)
        {
            return;
        }

        _targetDamage = Get<IDamage>.From(_targetCollider.gameObject);
        _targetDamage?.DealDamage(10);
        _targetDamage.VisualizeHit(_hit.point);
    }

    /// <summary>
    /// Toggles the muzzle flash particle effect.
    /// </summary>
    private void ToggleMuzzleFlash()
    {
        _muzzleFlash.Play();
    }

    /// <summary>
    /// Plays the sound effect for shooting.
    /// </summary>
    private void PlaySoundEffect()
    {
        ExplosionsSoundController.PlaySound(0, 0);
    }

    /// <summary>
    /// Initializes the raycast parameters.
    /// </summary>
    private void InitializeRaycastParameters()
    {
        _contactFilter = new ContactFilter2D { useLayerMask = true, layerMask = _targetLayerMask };
        _raycastHits = new RaycastHit2D[10];
    }

    /// <summary>
    /// Detects the target collider using raycast and performs necessary actions based on the result.
    /// </summary>
    private void DetectTargetCollider()
    {
        int hits = Physics2D.Raycast(transform.position, transform.right, _contactFilter, _raycastHits, 10f);
        
        if(hits < 1)
        {          
            AssignTargetCollider(null);
            Reference.Manager.ShootTargetUI.Deactivate();
            return;
        }

        foreach (var hit in _raycastHits)
        {
            bool isColliderValidAndNotTrigger = hit.collider != null && !hit.collider.isTrigger;

            if (isColliderValidAndNotTrigger)
            {
                CacheRaycastHit(hit);
                AssignTargetCollider(hit.collider);
                Reference.Manager.ShootTargetUI.Activate(_targetCollider.transform.position);
                Debug.DrawRay((Vector2)transform.position, (Vector2)transform.right * 10f, Color.red);
                return;
            }
        }
    }

    /// <summary>
    /// Caches the RaycastHit2D for later use.
    /// </summary>
    /// <param name="raycastHit">The RaycastHit2D to cache.</param>
    private void CacheRaycastHit(RaycastHit2D raycastHit)
    {
        _hit = raycastHit;
    }

    /// <summary>
    /// Assigns the target collider.
    /// </summary>
    /// <param name="collider">The collider of the target.</param>
    private void AssignTargetCollider(Collider2D collider)
    {
        _targetCollider = collider;
    }
} 