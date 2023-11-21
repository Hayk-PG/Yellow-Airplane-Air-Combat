using UnityEngine;
using Pautik;

public class HUDManager : MonoBehaviour
{
    [Header("Animator")]
    [SerializeField] private Animator _animator;

    private const string _hudFade = "HUDFade";




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

        Conditions<bool>.Compare((bool)data[0], () => FadeHUD(), () => FadeHUD(-1f, 1f));
    }

    private void FadeHUD(float speed = 1f, float normalizedTime = 0f)
    {
        _animator.Play(_hudFade, 0, normalizedTime);
        _animator.speed = speed;
    }
}