using UnityEngine;
using Pautik;

public class HUDManager : MonoBehaviour
{
    [Header("Animator")]
    [SerializeField] private Animator _animator;

    private const string _fadeHUDOut = "FadeHUDOut";
    private const string _fadeHUDIn = "FadeHUDIn";




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
        HandlePauseButtonClick(gameEventType, data);
    }

    private void HandlePauseButtonClick(GameEventType gameEventType, object[] data)
    {
        if (gameEventType != GameEventType.OnPauseButtonClick)
        {
            return;
        }

        Conditions<bool>.Compare((bool)data[0], () => FadeHUD(_fadeHUDOut), () => FadeHUD(_fadeHUDIn));
    }

    private void FadeHUD(string animationName)
    {
        _animator.Play(animationName);
    }
}