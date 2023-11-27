using UnityEngine;
using Pautik;

public class LoadingScreenManager : MonoBehaviour
{
    [Header("Canvas Group")]
    [SerializeField] private CanvasGroup _canvasGroup;

    [Header("Animator")]
    [SerializeField] private Animator _animator;




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
        HandleLoading(gameEventType);
        DisableLoadingOnSucceed(gameEventType);
        DisableLoadingOnFailed(gameEventType);
    }

    private void HandleLoading(GameEventType gameEventType)
    {
        if (gameEventType != GameEventType.UserRegistrationProceed && gameEventType != GameEventType.UserLoginProceed && gameEventType != GameEventType.LeaderboardProceed && gameEventType != GameEventType.UserStatisticsUpdateProceed)
        {
            return;
        }

        SetScreenActiveState(true);
    }

    private void DisableLoadingOnSucceed(GameEventType gameEventType)
    {
        if (gameEventType != GameEventType.UserRegistrationSucceed && gameEventType != GameEventType.UserLoginSucceed && gameEventType != GameEventType.LeaderboardSucceed && gameEventType != GameEventType.UserStatisticsUpdateSucceed)
        {
            return;
        }

        SetScreenActiveState(false);
    }

    private void DisableLoadingOnFailed(GameEventType gameEventType)
    {
        if (gameEventType != GameEventType.UserRegistrationFailed && gameEventType != GameEventType.UserLoginFailed && gameEventType != GameEventType.LeaderboardFailed && gameEventType != GameEventType.UserStatisticsUpdateFailed)
        {
            return;
        }

        SetScreenActiveState(false);
    }

    private void SetScreenActiveState(bool isActive)
    {
        _animator.SetBool("animate", isActive);

        GlobalFunctions.CanvasGroupActivity(_canvasGroup, isActive);      
    }
}