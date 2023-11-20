using UnityEngine;

public class SettingsAnimationManager : MonoBehaviour
{
    [Header("Animator")]
    [SerializeField] private Animator _animator;

    private const string _maskFadeIn = "MaskFadeIn";




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

        Invoke(nameof(FadeMaskIn), 3f);
    }

    private void FadeMaskIn()
    {
        _animator.Play(_maskFadeIn, 0, 0);
    }
}