using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    [Header("Animator")]
    [SerializeField] private Animator _animator;

    [Header("Text")]
    [SerializeField] private TMP_Text _scoreText;

    private WaitForSecondsRealtime _scoreTextAnimationInterval = new WaitForSecondsRealtime(0.05f);

    private object[] _data = new object[1];

    private const string _gameOverScreenFade = "GameOverScreenFade";
    private const string _gameOverTextFade = "GameOverTextFade";
    private const string _scoreTextBounceAndMoveLeft = "ScoreTextBounceAndMoveLeft";

    private int _playerFinalScore;




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
        DisplayGameOverScreen(gameEventType);
        TrackPlayerFinalScore(gameEventType, data);
    }

    private void DisplayGameOverScreen(GameEventType gameEventType)
    {
        if (gameEventType != GameEventType.OnPlayerAirplaneDestroy)
        {
            return;
        }

        SoundOverrider.UpdateSoundTrackVolume(SoundController.MusicVolume.Up, 0.05f);
        AnimateGameOverScreen();       
    }

    private void TrackPlayerFinalScore(GameEventType gameEventType, object[] data)
    {
        if (gameEventType != GameEventType.OnScored)
        {
            return;
        }

        _playerFinalScore = ((IScore)data[0]).Score;
    }

    private void AnimateGameOverScreen()
    {       
        _animator.Play(_gameOverScreenFade, 0, 0);
    }

    // Animation Event
    private void AnimateGameOverText()
    {
        _animator.Play(_gameOverTextFade, 1, 0);
    }

    // Animation Event
    private void StartScoreTextAnimation()
    {
        StartCoroutine(AnimateScoreText());
    }

    // Animation Event
    private void FinalizeGameOverScreen()
    {
        _data[0] = _playerFinalScore;
        GameEventHandler.RaiseEvent(GameEventType.OnGameOverScreenFinalize, _data);
    }

    private IEnumerator AnimateScoreText()
    {
        int current = 0;
        int increment = 0;

        while (current < _playerFinalScore)
        {
            increment = Mathf.RoundToInt((_playerFinalScore - current) * 0.05f) + 1;
            current += increment;
            _scoreText.text = $"Score: {current}";
            yield return _scoreTextAnimationInterval;
        }

        _animator.Play(_scoreTextBounceAndMoveLeft, 2, 0);
    }
}
