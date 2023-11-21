using UnityEngine;

public class SettingsAnimationManager : MonoBehaviour
{
    [Header("Animator")]
    [SerializeField] private Animator _animator;

    private const string _maskFadeIn = "MaskFadeIn";
    private const string _buttonsGroupFadeIn = "ButtonsGroupFadeIn";
    private const string _buttonsGroupFadeOut = "ButtonsGroupFadeOut";




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

    internal void FadeButtonsGroupIn()
    {
        _animator.Play(_buttonsGroupFadeIn, 1);
    }

    internal void FadeButtonsGroupOut()
    {
        _animator.Play(_buttonsGroupFadeOut, 1);
    }
}