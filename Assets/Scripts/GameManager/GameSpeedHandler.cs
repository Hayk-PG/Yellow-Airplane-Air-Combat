using System.Collections.Generic;
using UnityEngine;

public class GameSpeedHandler : MonoBehaviour
{
    private List<GameEventType> _gameSpeedChangeRequests = new List<GameEventType>();




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
        ResetGameSpeed(gameEventType);
        OnLastHopeDefenderMessageActivity(gameEventType, data);
        HandlePauseButtonClick(gameEventType);
    }

    private void ResetGameSpeed(GameEventType gameEventType)
    {
        if (gameEventType != GameEventType.ResetGameSpeed)
        {
            return;
        }

        _gameSpeedChangeRequests.Clear();
        ProcessPendingGameSpeedRequests();
    }

    private void OnLastHopeDefenderMessageActivity(GameEventType gameEventType, object[] data)
    {
        if (gameEventType != GameEventType.OnLastHopeDefenderMessageActivity)
        {
            return;
        }

        if (!((bool?)data[0]).HasValue)
        {
            return;
        }

        ManageGameSpeedRequest(gameEventType);
    }

    private void HandlePauseButtonClick(GameEventType gameEventType)
    {
        if (gameEventType != GameEventType.OnPauseButtonClick)
        {
            return;
        }

        ManageGameSpeedRequest(gameEventType);
    }

    private void ManageGameSpeedRequest(GameEventType gameEventType)
    {
        if (_gameSpeedChangeRequests.Contains(gameEventType))
        {
            _gameSpeedChangeRequests.Remove(gameEventType);
        }
        else
        {
            _gameSpeedChangeRequests.Add(gameEventType);
        }

        ProcessPendingGameSpeedRequests();
    }

    private void ProcessPendingGameSpeedRequests()
    {
        ToggleGamePause(isPaused: _gameSpeedChangeRequests.Count > 0);
    }

    private void ToggleGamePause(bool isPaused)
    {
        Time.timeScale = isPaused ? 0f : 1f;
    }
}