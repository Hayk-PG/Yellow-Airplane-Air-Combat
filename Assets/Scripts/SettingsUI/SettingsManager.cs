using UnityEngine;
using Pautik;

public class SettingsManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private SettingsInputManager _settingsInputManager;
    [SerializeField] private SettingsAnimationManager _settingsAnimationManager;




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
        HandleGameOverScreenFinalization(gameEventType);
        HandlePauseButtonClick(gameEventType, data);
    }

    private void HandleGameOverScreenFinalization(GameEventType gameEventType)
    {
        if (gameEventType != GameEventType.OnGameOverScreenFinalize)
        {
            return;
        }

        ActivateButtonsByIndex(new int[] { 1, 2, 3, 5 });
        _settingsAnimationManager.FadeButtonsGroupIn();
    }

    private void HandlePauseButtonClick(GameEventType gameEventType, object[] data)
    {
        if (gameEventType != GameEventType.OnPauseButtonClick)
        {
            return;
        }
       
        Conditions<bool>.Compare((bool)data[0], 
            delegate 
            {
                ActivateButtonsByIndex(new int[] { 0, 1, 3 });
                _settingsAnimationManager.FadeButtonsGroupIn();
            },
            delegate
            {
                _settingsAnimationManager.FadeButtonsGroupOut();
            });      
    }

    private void ActivateButtonsByIndex(int[] buttonsIndexesToActivate)
    {
        for (int i = 0; i < _settingsInputManager._buttons.Length; i++)
        {
            _settingsInputManager._buttons[i].Deselect();

            for (int j = 0; j < buttonsIndexesToActivate.Length; j++)
            {
                if(i == buttonsIndexesToActivate[j])
                {
                    _settingsInputManager._buttons[i].gameObject.SetActive(true);
                    break;
                }

                _settingsInputManager._buttons[i].gameObject.SetActive(false);
            }
        }
    }
}
