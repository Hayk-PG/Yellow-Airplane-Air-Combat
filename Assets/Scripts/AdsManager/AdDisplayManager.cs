using UnityEngine;

public class AdDisplayManager : MonoBehaviour
{
    private int _adDisplayCount;




    private void OnEnable()
    {
        GameEventHandler.OnEvent += OnGameEvent;
    }

    private void OnDisable()
    {
        GameEventHandler.OnEvent -= OnGameEvent;
    }

    private void OnGameEvent(GameEventType gameEventType, object[] data)
    {
        HandlePlayerAirplaneDestroy(gameEventType);
    }

    private void HandlePlayerAirplaneDestroy(GameEventType gameEventType)
    {
        if (gameEventType != GameEventType.OnPlayerAirplaneDestroy)
        {
            return;
        }

        _adDisplayCount++;

        if (_adDisplayCount < 2)
        {
            return;
        }

        GameEventHandler.RaiseEvent(GameEventType.DisplayInterstitialAd);
        _adDisplayCount = 0;
    }
}