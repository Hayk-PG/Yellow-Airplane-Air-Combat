using System;
using UnityEngine;

public class PlayerEventSystem : MonoBehaviour
{
    public event Action<PlayerEventType, object[]> OnPlayerEventTrigger;




    /// <summary>
    /// Triggers a player event with the specified event type and optional data.
    /// </summary>
    /// <param name="playerEventType">The type of player event to trigger.</param>
    /// <param name="data">Optional data associated with the event.</param>
    public void TriggerPlayerEvent(PlayerEventType playerEventType, object[] data = null)
    {
        OnPlayerEventTrigger?.Invoke(playerEventType, data);
    }
}
