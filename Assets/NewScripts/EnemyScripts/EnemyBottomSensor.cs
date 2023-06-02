
public class EnemyBottomSensor : BaseEnemySenseManager
{
    protected override void SetTargetChasingConditions(bool isAllowedToChase)
    {
        
    }

    protected override void DetectCollision(bool isCollision)
    {
        if (IsEnemyMovementManagerNull)
        {
            return;
        }

        _enemyMovementManager.HasBottomCollision = isCollision;
    }
} 
