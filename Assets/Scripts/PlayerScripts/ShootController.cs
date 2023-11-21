using UnityEngine;
using Pautik;

public class ShootController : BaseShootController
{
    [Header("Components")]
    [SerializeField] private ScoreManager _scoreController;

    private float _defaultFireRate;
    private float _gunHeatElapsedTime;
    private float _gunHeatThreshold = 1f;




    protected override void Awake()
    {
        base.Awake();
        DefineDefaultFireRate();
    }

    private void OnEnable()
    {
        Reference.Manager.InputController.OnInputController += OnInputController;
    }

    private void OnDisable()
    {
        Reference.Manager.InputController.OnInputController -= OnInputController;
    }

    /// <summary>
    /// Handles the subscription to input events.
    /// </summary>
    /// <param name="inputType">The type of input event.</param>
    /// <param name="data">Additional data associated with the input event.</param>
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
    }

    /// <summary>
    /// Controls the gun heat and adjusts the fire rate accordingly.
    /// </summary>
    private void AdjustFireRateBasedOnHeat()
    {
        if (_isShooting)
        {
            bool isGunHeating = _gunHeatElapsedTime >= _gunHeatThreshold;

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
            }
        }
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