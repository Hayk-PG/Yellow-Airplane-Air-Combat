
public class EnemyFrontSensor : BaseEnemySenseManager 
{
    protected override void DetectCollision(bool isCollision)
    {
        if(IsEnemyMovementManagerNull)
        {
            return;
        }

        _enemyMovementManager.HasFrontCollision = isCollision;
    }
}