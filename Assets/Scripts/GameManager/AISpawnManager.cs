using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISpawnManager : MonoBehaviour
{
    [Header("Enemy Airplane Prefab")]
    [SerializeField] private AIMovementManager _aiAirplanePrefab;

    private WaitForSeconds _airplaneSpawnCheckInterval = new WaitForSeconds(2f);
    private WaitForSeconds _airplaneSpawnInterval = new WaitForSeconds(1f);
    private Transform _playerAirplane;
    private List<AIMovementManager> _aiAirplanes = new List<AIMovementManager>();

    public event System.Action<AISpawnManagerCommand, AIMovementManager> OnManageList;




    private void Start()
    {
        GetPlayerAirplane();
        StartCoroutine(CheckAirplaneSpawn());
    }

    private void GetPlayerAirplane()
    {
        _playerAirplane = FindObjectOfType<MovementController>().transform;
    }

    /// <summary>
    /// Periodically checks if there are any AI airplanes present and initiates spawning if there aren't any.
    /// </summary>
    /// <returns>An enumerator for the coroutine.</returns>
    private IEnumerator CheckAirplaneSpawn()
    {
        while (_playerAirplane != null)
        {
            yield return _airplaneSpawnCheckInterval;

            if (_aiAirplanes.Count == 0)
            {
                StartCoroutine(Spawn());
            }
        }
    }

    /// <summary>
    /// Spawns AI airplanes until the maximum count is reached.
    /// </summary>
    /// <returns>An enumerator for the coroutine.</returns>
    private IEnumerator Spawn()
    {
        while (_aiAirplanes.Count < 3 && _playerAirplane != null)
        {
            float randomX = Random.Range(10f, 20f);
            float randomY = Random.Range(10f, 20f);
            float randomHorizontalOffset = Random.Range(-1f, 1f) <= 0 ? -randomX : randomX;
            float randomVerticalOffset = Random.Range(-1f, 1f) <= 0 ? -randomY : randomY;

            InstantiateAIAirplane(randomHorizontalOffset, randomVerticalOffset);
            yield return _airplaneSpawnInterval;
        }
    }

    /// <summary>
    /// Instantiates an AI airplane at the specified position and adds it to the AI airplane list.
    /// </summary>
    /// <param name="randomHorizontalOffset">The random horizontal offset for the AI airplane spawn position.</param>
    /// <param name="randomVerticalOffset">The random vertical offset for the AI airplane spawn position.</param>
    private void InstantiateAIAirplane(float randomHorizontalOffset, float randomVerticalOffset)
    {
        AIMovementManager aiAirplane = Instantiate(_aiAirplanePrefab, (Vector2)_playerAirplane.position + new Vector2(randomHorizontalOffset, randomVerticalOffset), Quaternion.identity);
        AddAirplaneToList(aiAirplane);
    }

    /// <summary>
    /// Adds an AI airplane to the list and raises the manage list event.
    /// </summary>
    /// <param name="aIMovementManager">The AI movement manager representing the airplane to add.</param>
    private void AddAirplaneToList(AIMovementManager aIMovementManager)
    {
        _aiAirplanes.Add(aIMovementManager);

        RaiseManageListEvent(AISpawnManagerCommand.AddedToList, aIMovementManager);
    }

    /// <summary>
    /// Removes an AI airplane from the list and raises the manage list event.
    /// </summary>
    /// <param name="aIMovementManager">The AI movement manager representing the airplane to remove.</param>
    public void RemoveAirplaneFromList(AIMovementManager aIMovementManager)
    {
        _aiAirplanes.Remove(aIMovementManager);

        RaiseManageListEvent(AISpawnManagerCommand.RemovedFromList, aIMovementManager);
    }

    /// <summary>
    /// Raises the manage list event with the specified command and AI movement manager.
    /// </summary>
    /// <param name="aISpawnManagerCommand">The command indicating whether the AI movement manager was added or removed.</param>
    /// <param name="aIMovementManager">The AI movement manager that was added or removed.</param>
    private void RaiseManageListEvent(AISpawnManagerCommand aISpawnManagerCommand, AIMovementManager aIMovementManager)
    {
        OnManageList?.Invoke(aISpawnManagerCommand, aIMovementManager);
    }
}