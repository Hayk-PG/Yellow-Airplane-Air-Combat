
public interface IEnemyMovementManager 
{
    bool HasFrontCollision { get; set; }
    bool HasTopCollision { get; set; }
    bool HasBottomCollision { get; set; }
    bool CanChaseTarget { get; set; }
}
