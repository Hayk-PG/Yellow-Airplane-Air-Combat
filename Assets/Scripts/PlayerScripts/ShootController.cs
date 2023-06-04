using UnityEngine;
using Pautik;

public class ShootController : BaseShootController
{
    [Header("Components")]
    [SerializeField] private ScoreManager _scoreController;

    


    private void OnEnable()
    {
        Reference.Manager.InputController.OnInputController += OnInputController;
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

        SetShootTargetUIActive(true);
        UpdateScore();
    }

    private void SetShootTargetUIActive(bool isActive, bool isShaking = false)
    {
        if (!isActive)
        {
            Reference.Manager.ShootTargetUI.Deactivate();
            return;
        }

        if (isShaking)
        {
            Reference.Manager.ShootTargetUI.PlayIconShakeEffect();
        }
        else
        {
            Reference.Manager.ShootTargetUI.Activate(_targetCollider.transform.position);
        }      
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
        SetShootTargetUIActive(false);
    }

    protected override void OnValidateTargetCollider(RaycastHit2D hit)
    {
        base.OnValidateTargetCollider(hit);
        SetShootTargetUIActive(true, true);
    }
} 