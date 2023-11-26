using UnityEngine;

public class GameInitVerifier : MonoBehaviour
{
    private bool[] _assetsLoadStatus = new bool[4];




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
        HandleAssetStatus(gameEventType, GameEventType.BackgroundSpritesInit, 0);
        HandleAssetStatus(gameEventType, GameEventType.CloudSpritesInit, 1);
        HandleAssetStatus(gameEventType, GameEventType.CrosshairSpriteInit, 2);
        HandleAssetStatus(gameEventType, GameEventType.AirplaneAssetInit, 3);
    }

    private void HandleAssetStatus(GameEventType gameEventType, GameEventType targetEventType, int assetLoadStatusIndex)
    {
        if (gameEventType != targetEventType)
        {
            return;
        }

        _assetsLoadStatus[assetLoadStatusIndex] = true;
        TrySwitchMenuScene();
    }

    private void TrySwitchMenuScene()
    {
        foreach (var status in _assetsLoadStatus)
        {
            if (!status)
            {
                return;
            }
        }

        MyScene.Manager.LoadSceneAsync(0);
    }
}