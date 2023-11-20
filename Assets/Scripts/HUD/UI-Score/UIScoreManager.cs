using UnityEngine;
using TMPro;
using Pautik;

public class UIScoreManager : MonoBehaviour
{
    [Header("Animator")]
    [SerializeField] private Animator _animator;

    [Header("Text")]
    [SerializeField] private TMP_Text _scoreText;

    private const string _animationName = "UI Score Text Anim";




    private void Awake()
    {
        InitializeScoreText();
    }

    /// <summary>
    /// Initializes the score text by setting the initial value and color.
    /// </summary>
    private void InitializeScoreText()
    {
        _scoreText.text = $"{0}";
    }

    /// <summary>
    /// Updates the score text with the specified score value.
    /// </summary>
    /// <param name="score">The new score value.</param>
    public void UpdateScoreText(int score)
    {
        _scoreText.text = $"{Converter.ThousandsSeparatorString(score)}";
        _animator.Play(_animationName, 0, 0);
    }
}