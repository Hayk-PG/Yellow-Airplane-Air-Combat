using System.Collections;
using UnityEngine;

public class RepairStationSpawnManager : MonoBehaviour
{
    [Header("Repair Station Gameobject Prefab")]
    [SerializeField] private GameObject _repairStationPrefab;

    private PlayerHealthManager _playerHealthManager;

    private bool _canSpawnRepairStation = true;




    private void OnEnable()
    {
        Reference.Manager.PlayerEventSystem.OnPlayerEventTrigger += OnPlayerEventTrigger;
    }

    /// <summary>
    /// Handles the event triggered by the player.
    /// </summary>
    /// <param name="playerEventType">The type of player event.</param>
    /// <param name="data">The event data.</param>
    private void OnPlayerEventTrigger(PlayerEventType playerEventType, object[] data)
    {
        GetPlayerHealthManager(playerEventType, data);
    }

    /// <summary>
    /// Retrieves the player's health manager from the event data.
    /// </summary>
    /// <param name="playerEventType">The type of player event.</param>
    /// <param name="data">The event data.</param>
    private void GetPlayerHealthManager(PlayerEventType playerEventType, object[] data)
    {
        if(playerEventType != PlayerEventType.PublishPlayerHealthManager)
        {
            return;
        }

        _playerHealthManager = (PlayerHealthManager)data[1];
    }

    /// <summary>
    /// Tries to spawn a repair station if one doesn't already exist in the scene.
    /// </summary>
    public void TrySpawnRepairStation()
    {
        bool repairStationManagerExists = FindObjectOfType<RepairStationManager>() != null;

        if (repairStationManagerExists)
        {
            return;
        }

        InstantiateRepairStationGameobject();
    }

    /// <summary>
    /// Runs the countdown timer for allowing repair station spawning.
    /// </summary>
    public void RunRepairStationSpawnTimerCountdown()
    {
        StartCoroutine(AllowRepairStationSpawnAfterDelay());
    }

    /// <summary>
    /// Instantiates a repair station game object at a random position near the player's position.
    /// </summary>
    private void InstantiateRepairStationGameobject()
    {
        bool canInstantiateNewRepairStation = _playerHealthManager != null && _playerHealthManager.Health < 90 && _canSpawnRepairStation;

        if (!canInstantiateNewRepairStation)
        {
            return;
        }

        float randomX = Random.Range(3f, 5f);
        float randomY = Random.Range(3f, 5f);
        float adjustedHorizontalPosition = Random.Range(0, 2) > 0 ? randomX : -randomX;
        float adjustedVerticalPosition = Random.Range(0, 2) > 0 ? randomY : -randomY;
        Vector2 position = new Vector2(_playerHealthManager.transform.position.x + adjustedHorizontalPosition, _playerHealthManager.transform.position.y + adjustedVerticalPosition);

        Instantiate(_repairStationPrefab, position, Quaternion.identity);      
    }

    /// <summary>
    /// Delays the ability to spawn a repair station for a specified duration.
    /// </summary>
    /// <returns>An IEnumerator representing the coroutine.</returns>
    private IEnumerator AllowRepairStationSpawnAfterDelay()
    {
        float remainingTime = 120f;
        float elapsedTime = 1f;

        while (remainingTime > elapsedTime)
        {
            remainingTime -= 1f;
            yield return new WaitForSeconds(1f);
        }

        _canSpawnRepairStation = true;
    }
}