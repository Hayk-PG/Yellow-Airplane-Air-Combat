using System;
using UnityEngine;
using Pautik;

public abstract class BaseEnemySenseManager : MonoBehaviour
{
    protected IEnemyMovementManager _enemyMovementManager;

    protected bool IsEnemyMovementManagerNull => _enemyMovementManager == null;



    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (!IsEnemyMovementManagerNull)
        {
            return;
        }

        GetCollidedEnemyMovementManager(collision.gameObject);
        SetTargetChasingConditions(false);
        DetectCollision(true);
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (_enemyMovementManager != CollidedEnemyMovementManager(collision.gameObject))
        {
            return;
        }

        SetTargetChasingConditions(true);
        DetectCollision(false);
        ReleaseCollidedEnemyMovementManager();
    }

    protected virtual void GetCollidedEnemyMovementManager(GameObject gameObject)
    {
        _enemyMovementManager = CollidedEnemyMovementManager(gameObject);
    }

    protected virtual void SetTargetChasingConditions(bool isAllowedToChase)
    {
        if(IsEnemyMovementManagerNull)
        {
            return;
        }

        _enemyMovementManager.CanChaseTarget = isAllowedToChase;
    }

    protected virtual void ReleaseCollidedEnemyMovementManager()
    {
        _enemyMovementManager = null;
    }

    protected virtual IEnemyMovementManager CollidedEnemyMovementManager(GameObject gameObject)
    {
        return Get<IEnemyMovementManager>.From(gameObject);
    }

    protected abstract void DetectCollision(bool isCollision);
}