using UnityEngine;

public class ScoreManager : MonoBehaviour, IScore
{
    [SerializeField] private int _score;

    public int Score
    {
        get => _score;
        set => _score = value;
    }




    public void UpdateScore(int value)
    {
        bool isValueAcceptable = value != 0;

        if (!isValueAcceptable)
        {
            return;
        }

        _score += value;

        UpdateUIScore(_score);
    }

    /// <summary>
    /// Updates the UI score display with the provided score value.
    /// </summary>
    /// <param name="score">The score value to display.</param>
    private void UpdateUIScore(int score)
    {
        Reference.Manager.UIScoreManager.UpdateScoreText(score);
    }
}