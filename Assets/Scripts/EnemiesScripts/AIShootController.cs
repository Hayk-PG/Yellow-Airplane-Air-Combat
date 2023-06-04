using UnityEngine;
using Pautik;

public class AIShootController : BaseShootController
{
    [Header("Self Layer Mask")]
    [SerializeField] private LayerMask _aiLayerMask;




    protected override void Update()
    {
        base.Update();

        if (_isShooting)
        {
            TryRunCoroutine();
        }
    }

    protected override int RaycastHitsCount()
    {
        bool isAiBlockingSight = Converter.RaycastHit2D(transform.position + transform.right * 2, transform.right, 5, _aiLayerMask);
        return isAiBlockingSight ? 0 : base.RaycastHitsCount();
    }

    protected override void OnNullHits()
    {
        base.OnNullHits();
        _isShooting = false;
    }

    protected override void OnValidateTargetCollider(RaycastHit2D hit)
    {
        base.OnValidateTargetCollider(hit);       
        _isShooting = true;
    }

    protected override void Shoot()
    {
        if (_targetCollider == null)
        {
            return;
        }

        _targetDamage = Get<IDamage>.From(_targetCollider.gameObject);
        _targetDamage?.DealDamage(5);
    }
} 