using UnityEngine;
using Pautik;

public class ShootController : BaseShootController
{
    [Header("Components")]
    [SerializeField] private ScoreManager _scoreController;

    private object[] _fireRateData = new object[1];
    private float _defaultFireRate;
    private float _gunHeatElapsedTime;
    private float _gunHeatThreshold = 1f;




    protected override void Awake()
    {
        base.Awake();
        Invoke(nameof(DefineDefaultFireRate), 0.02f);
    }

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
        HandleShootInput(gameEventType, data);
    }

    private void HandleShootInput(GameEventType gameEventType, object[] data)
    {
        if (gameEventType != GameEventType.OnShootButtonState)
        {
            return;
        }

        HandleShootInput(isShooting: (bool)data[0]);
    }

    private void HandleShootInput(bool isShooting) 
    {
        _isShooting = isShooting;
        TryRunCoroutine();
        AdjustFireRateBasedOnHeat();
    }

    protected override void Shoot()
    {
        if (_targetCollider == null)
        {
            return;
        }

        _targetDamage = Get<IDamage>.From(_targetCollider.gameObject);
        _targetDamage?.DealDamage(10, _scoreController);
        _targetDamage.VisualizeHit(_hit.point);

        ShakeUITarget();
        UpdateScore();
    }

    private void DefineDefaultFireRate()
    {
        _defaultFireRate = _fireRate;
        GunfireBroadcast(GameEventType.OnGunfireDefaultRateInit);
    }

    /// <summary>
    /// Controls the gun heat and adjusts the fire rate accordingly.
    /// </summary>
    private void AdjustFireRateBasedOnHeat()
    {
        if (_isShooting)
        {         
            bool isGunHeating = _gunHeatElapsedTime >= _gunHeatThreshold;
            GunfireBroadcast(GameEventType.OnGunFire);

            if (isGunHeating)
            {
                _fireRate = _fireRate > 0f ? Mathf.Lerp(_fireRate, _fireRate - Mathf.RoundToInt(_gunHeatElapsedTime), 100f * Time.deltaTime) : 0f;
                return;
            }

            _gunHeatElapsedTime += Time.deltaTime;
        }
        else
        {
            bool isGunCooling = _gunHeatElapsedTime > 0f;

            if (isGunCooling)
            {
                _gunHeatElapsedTime = 0f;
                _fireRate = _defaultFireRate;               
                GunfireBroadcast(GameEventType.OnGunFire);
            }
        }
    }

    private void GunfireBroadcast(GameEventType gameEventType)
    {
        _fireRateData[0] = _fireRate;
        GameEventHandler.RaiseEvent(gameEventType, _fireRateData);
    }

    /// <summary>
    /// Sets the visibility of the shoot target UI.
    /// </summary>
    /// <param name="isActive">True to activate the UI, false to deactivate it.</param>
    private void SetUITargetActive(bool isActive)
    {
        if (!isActive)
        {
            Reference.Manager.ShootTargetUI.Deactivate();
            return;
        }

        Reference.Manager.ShootTargetUI.Activate(_targetCollider.transform.position);
    }

    /// <summary>
    /// Shakes the shoot target UI to create an effect.
    /// </summary>
    private void ShakeUITarget()
    {
        Reference.Manager.ShootTargetUI.PlayIconShakeEffect();
    }

    /// <summary>
    /// Updates the score by generating a random value between 0 and 10 and updating the score controller.
    /// </summary>
    private void UpdateScore()
    {
        int score = Random.Range(0, 11);
        _scoreController.UpdateScore(score);
    }

    protected override void OnNullHits()
    {
        base.OnNullHits();
        SetUITargetActive(false);
    }

    protected override void OnValidateTargetCollider(RaycastHit2D hit)
    {
        base.OnValidateTargetCollider(hit);
        SetUITargetActive(true);
    }
} 