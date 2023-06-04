using UnityEngine;

public class AIHealthManager : BaseHealthManager
{
    [Header("Entity Components")]
    [SerializeField] private AirplaneRemovalController _airplaneRemovalController;




    public override void DealDamage(int damage, IScore attackerScore = default)
    {
        IncrementKillScore(attackerScore);
        RemoveFromSpawnerList();
        base.DealDamage(damage);
    }

    /// <summary>
    /// Removes the AI airplane from the spawner list.
    /// </summary>
    private void RemoveFromSpawnerList()
    {
        _airplaneRemovalController.RemoveFromSpawnerList();
    }
}