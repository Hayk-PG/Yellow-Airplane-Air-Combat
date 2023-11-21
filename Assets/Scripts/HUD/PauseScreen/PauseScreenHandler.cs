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
        _pauseButton.OnSelect += OnPauseButtonClick;
    }

    private void OnPauseButtonClick()
    {
        PauseButtonClickBroadcast(true);
        SetCanvasGroupActive(_backgroundCanvasGroup, true);
        SetCanvasGroupActive(_pauseButtonCanvasGroup, false);        
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