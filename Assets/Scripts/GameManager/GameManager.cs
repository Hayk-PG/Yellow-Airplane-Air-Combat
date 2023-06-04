using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Event that is raised when a GameManager event occurs.
    /// </summary>
    public event Action<GameManagerEventType> OnGameManager;




    private void Start()
    {
        RaiseGameManagerEvent(GameManagerEventType.GameStart);
    }

    /// <summary>
    /// Raises a GameManager event.
    /// </summary>
    /// <param name="gameManagerEventType">The type of GameManager event.</param>
    public void RaiseGameManagerEvent(GameManagerEventType gameManagerEventType)
    {
        OnGameManager?.Invoke(gameManagerEventType);
    }
}