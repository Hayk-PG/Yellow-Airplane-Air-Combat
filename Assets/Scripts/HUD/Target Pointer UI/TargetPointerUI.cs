using UnityEngine;
using Pautik;
using System.Collections.Generic;

public class TargetPointerUI  : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private RectTransform _pointerIcon;

    private Transform _player;
    private List<Transform> _targets = new List<Transform>();

    


    private void OnEnable()
    {
        Reference.Manager.GameManager.OnGameManager += OnGameManager;
        Reference.Manager.AISpawnManager.OnManageList += OnAISpawnManagerList;
    }

    private void Update()
    {
        RotatePointer();
    }

    /// <summary>
    /// Rotates the pointer icon based on the player's position and the target's position.
    /// </summary>
    private void RotatePointer()
    {
        if (_player == null || _targets.Count == 0)
        {
            return;
        }

        Vector3 targetPosition = _targets[0].position;
        Vector2 direction = Converter.Direction(targetPosition, _player.position);
        float distance = Converter.Distance(_player.position, targetPosition);
        float angle = Converter.RadianAngle(direction);
        float alpha = Mathf.Lerp(0, distance, 0.02f);

        _pointerIcon.transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
        _canvasGroup.alpha = alpha;
    }

    /// <summary>
    /// Handles the GameManager event.
    /// </summary>
    /// <param name="gameManagerEventType">The type of GameManager event.</param>
    private void OnGameManager(GameManagerEventType gameManagerEventType)
    {
        GetPlayerTransform(gameManagerEventType);
    }

    /// <summary>
    /// Handles the event raised by the AISpawnManager when an AI movement manager is added to or removed from the list.
    /// </summary>
    /// <param name="aISpawnManagerCommand">The command indicating whether the AI movement manager was added or removed.</param>
    /// <param name="aIMovementManager">The AI movement manager that was added or removed.</param>
    private void OnAISpawnManagerList(AISpawnManagerCommand aISpawnManagerCommand, AIMovementManager aIMovementManager)
    {
        if(aISpawnManagerCommand == AISpawnManagerCommand.AddedToList)
        {
            _targets.Add(aIMovementManager.transform);
        }
        else if(aISpawnManagerCommand == AISpawnManagerCommand.RemovedFromList)
        {
            _targets.Remove(aIMovementManager.transform);
        }
    }

    /// <summary>
    /// Gets the player's transform when the game starts.
    /// </summary>
    /// <param name="gameManagerEventType">The type of GameManager event.</param>
    private void GetPlayerTransform(GameManagerEventType gameManagerEventType)
    {
        if (gameManagerEventType != GameManagerEventType.GameStart)
        {
            return;
        }

        _player = FindObjectOfType<MovementController>().transform;
    }
}