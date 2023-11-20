using UnityEngine;

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
    }

    private void HandleGameOverScreenFinalization(GameEventType gameEventType)
    {
        if (gameEventType != GameEventType.OnGameOverScreenFinalize)
        {
            return;
        }

        ActivateButtonsByIndex(new int[] { 1, 2, 3 });
        _settingsAnimationManager.FadeButtonsGroupIn();
    }

    private void ActivateButtonsByIndex(int[] buttonsIndexesToActivate)
    {
        for (int i = 0; i < _settingsInputManager._buttons.Length; i++)
        {
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
