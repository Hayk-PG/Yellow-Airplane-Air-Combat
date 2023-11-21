using UnityEngine;
using Pautik;

public class PauseScreenHandler : MonoBehaviour
{
    [Header("Canvas Group")]
    [SerializeField] private CanvasGroup _backgroundCanvasGroup;
    [SerializeField] private CanvasGroup _pauseButtonCanvasGroup;

    [Header("Button")]
    [SerializeField] private Btn _pauseButton;

    private object[] _data = new object[1];




    private void OnEnable()
    {     
        GameEventHandler.OnEvent += OnGameEvent;
        _pauseButton.OnSelect += TogglePauseScreenActiveState;
    }

    private void OnDisable()
    {
        GameEventHandler.OnEvent -= OnGameEvent;
    }

    private void OnGameEvent(GameEventType gameEventType, object[] data)
    {
        HandleResumeButtonClick(gameEventType);
    }

    private void HandleResumeButtonClick(GameEventType gameEventType)
    {
        if (gameEventType != GameEventType.OnResumeButtonClick)
        {
            return;
        }

        TogglePauseScreenActiveState();
    }

    private void TogglePauseScreenActiveState()
    {
        bool isPauseScreenActive = _backgroundCanvasGroup.interactable;
        SetCanvasGroupActive(_backgroundCanvasGroup, !isPauseScreenActive);
        SetCanvasGroupActive(_pauseButtonCanvasGroup, isPauseScreenActive);
        PauseButtonClickBroadcast(!isPauseScreenActive);
    }

    private void PauseButtonClickBroadcast(bool isClicked)
    {
        _data[0] = isClicked;
        GameEventHandler.RaiseEvent(GameEventType.OnPauseButtonClick, _data);
    }

    private void SetCanvasGroupActive(CanvasGroup canvasGroup, bool isActive)
    {
        GlobalFunctions.CanvasGroupActivity(canvasGroup, isActive);
    }
}