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
        TrySpawnRepairStationForPlayer();
    }

    /// <summary>
    /// Removes the AI airplane from the spawner list.
    /// </summary>
    private void RemoveFromSpawnerList()
    {
        _airplaneRemovalController.RemoveFromSpawnerList();
    }

    /// <summary>
    /// Tries to spawn a repair station for the player if their health is zero or below.
    /// </summary>
    private void TrySpawnRepairStationForPlayer()
    {
        if(Health > 0)
        {
            return;
        }

        Reference.Manager.RepairStationSpawnManager.TrySpawnRepairStation();
    }
}