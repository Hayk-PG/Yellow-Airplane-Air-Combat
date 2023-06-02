
public class EnemyTopSensor : BaseEnemySenseManager
{
    protected override void SetTargetChasingConditions(bool isAllowedToChase)
    {
        
    }

    protected override void DetectCollision(bool isCollision)
    {
        if(IsEnemyMovementManagerNull)
        {
            return;
        }

        _enemyMovementManager.HasTopCollision = isCollision;
    }
}