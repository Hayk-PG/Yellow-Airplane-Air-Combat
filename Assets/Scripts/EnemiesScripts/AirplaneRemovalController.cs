using UnityEngine;

public class AirplaneRemovalController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private AIMovementManager _aiMovementManager;




    /// <summary>
    /// Removes the AI airplane from the enemy spawner's airplane list.
    /// </summary>
    public void RemoveFromSpawnerList()
    {
        Reference.Manager.EnemySpawner.RemoveAirplaneFromList(_aiMovementManager);
    }
}