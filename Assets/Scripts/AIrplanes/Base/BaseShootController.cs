using System.Collections;
using UnityEngine;
using Pautik;

public abstract class BaseShootController : MonoBehaviour
{
    [Header("Shakeable Particle Systems")]
    [SerializeField] protected ShakeableParticleSystems _muzzleFlash;

    [Header("Target Layer Mask")]
    [SerializeField] protected LayerMask _targetLayerMask;

    protected RaycastHit2D[] _raycastHits;
    protected RaycastHit2D _hit;
    protected ContactFilter2D _contactFilter;
    protected Collider2D _targetCollider;
    protected IDamage _targetDamage;
    protected IEnumerator _fireRoutine;

    [Header("Rate Of Fire")]
    [SerializeField] protected float _fireRate = 650f;

    protected bool _isShooting;




    protected virtual void Awake()
    {
        InitializeRaycastParameters();
    }

    protected virtual void Update()
    {
        DetectTargetCollider();
    }

    /// <summary>
    /// Tries to run the shooting coroutine if it's not already running.
    /// </summary>
    protected virtual void TryRunCoroutine()
    {
        if (_fireRoutine == null)
        {
            _fireRoutine = FireRoutine(_isShooting);
            StartCoroutine(_fireRoutine);
        }
    }

    /// <summary>
    /// The coroutine for shooting and controlling the rate of fire.
    /// </summary>
    /// <param name="isShooting">Determines if the shooting is enabled or disabled.</param>
    protected virtual IEnumerator FireRoutine(bool isShooting)
    {
        while (_isShooting)
        {
            float fireInterval = 60f / _fireRate;

            Shoot();
            ToggleMuzzleFlash();
            PlaySoundEffect();
            yield return new WaitForSeconds(fireInterval);
        }

        _fireRoutine = null;
    }

    /// <summary>
    /// Performs the shooting action.
    /// </summary>
    protected abstract void Shoot();

    /// <summary>
    /// Toggles the muzzle flash effect.
    /// </summary>
    protected virtual void ToggleMuzzleFlash()
    {
        _muzzleFlash.Play();
    }

    /// <summary>
    /// Plays the sound effect associated with shooting.
    /// </summary>
    protected virtual void PlaySoundEffect()
    {
        ExplosionsSoundController.PlaySound(0, 0);
    }

    /// <summary>
    /// Initializes the parameters for raycasting and target detection.
    /// </summary>
    protected virtual void InitializeRaycastParameters()
    {
        _contactFilter = new ContactFilter2D { useLayerMask = true, layerMask = _targetLayerMask };
        _raycastHits = new RaycastHit2D[10];
    }

    /// <summary>
    /// Detects the collider of the target using raycasting.
    /// </summary>
    protected virtual void DetectTargetCollider()
    {
        int hits = RaycastHitsCount();

        if (hits < 1)
        {
            OnNullHits();
            return;
        }

        foreach (var hit in _raycastHits)
        {
            bool isColliderValidAndNotTrigger = hit.collider != null && !hit.collider.isTrigger;

            if (isColliderValidAndNotTrigger)
            {
                OnValidateTargetCollider(hit);
                return;
            }
        }
    }

    /// <summary>
    /// Returns the number of valid raycast hits.
    /// </summary>
    /// <returns>The number of valid raycast hits.</returns>
    protected virtual int RaycastHitsCount()
    {
        return Converter.RaycastHit2D(transform.position, transform.right, _contactFilter, _raycastHits, 10f);
    }

    /// <summary>
    /// Called when there are no valid raycast hits.
    /// </summary>
    protected virtual void OnNullHits()
    {
        AssignTargetCollider(null);
    }

    /// <summary>
    /// Called when a valid target collider is detected.
    /// </summary>
    /// <param name="hit">The RaycastHit2D object representing the hit.</param>
    protected virtual void OnValidateTargetCollider(RaycastHit2D hit)
    {
        CacheRaycastHit(hit);
        AssignTargetCollider(hit.collider);
    }

    /// <summary>
    /// Caches the latest raycast hit.
    /// </summary>
    /// <param name="raycastHit">The RaycastHit2D object to cache.</param>
    protected virtual void CacheRaycastHit(RaycastHit2D raycastHit)
    {
        _hit = raycastHit;
    }

    /// <summary>
    /// Assigns the target collider to be used for damage calculations.
    /// </summary>
    /// <param name="collider">The target collider to assign.</param>
    protected virtual void AssignTargetCollider(Collider2D collider)
    {
        _targetCollider = collider;
    }
}