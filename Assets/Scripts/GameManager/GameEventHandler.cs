using System;
using UnityEngine;

public class GameEventHandler : MonoBehaviour
{
    public static event Action<GameEventType, object[]> OnEvent;




    public static void RaiseEvent(GameEventType gameEventType, object[] data = null)
    {
        OnEvent?.Invoke(gameEventType, data);
    }
}