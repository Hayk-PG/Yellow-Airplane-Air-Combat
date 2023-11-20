using UnityEngine;

public class MainMenuAnimationManager : MonoBehaviour
{
    [Header("Animator")]
    [SerializeField] private Animator _animator;

    private const string _buttonsPopUp = "ButtonsPopUp";




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
        HandleMainMenuInitialization(gameEventType);
    }

    private void HandleMainMenuInitialization(GameEventType gameEventType)
    {
        if (gameEventType != GameEventType.OnMainMenuInit)
        {
            return;
        }

        Invoke(nameof(PlayButtonsPopUpAnimation), 3f);
    }

    private void PlayButtonsPopUpAnimation()
    {
        _animator.Play(_buttonsPopUp, 0, 0);
    }
}