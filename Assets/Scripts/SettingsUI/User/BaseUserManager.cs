using UnityEngine;
using Pautik;

public class BaseUserManager : MonoBehaviour
{
    protected virtual void OnEnable()
    {
        GameEventHandler.OnEvent += OnGameEvent;
    }

    protected virtual void OnDisable()
    {
        GameEventHandler.OnEvent -= OnGameEvent;
    }

    protected virtual void OnGameEvent(GameEventType gameEventType, object[] data)
    {
        return;
    }

    protected virtual void SetCanvasGroupActive(CanvasGroup canvasGroup, bool isActive = true)
    {
        GlobalFunctions.CanvasGroupActivity(canvasGroup, isActive);
    }
}