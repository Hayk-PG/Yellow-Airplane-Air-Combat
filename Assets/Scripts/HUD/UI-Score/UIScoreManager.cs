using UnityEngine;
using TMPro;
using Pautik;

public class UIScoreManager : MonoBehaviour
{
    [Header("Tmp Text Component")]
    [SerializeField] private TMP_Text _scoreText;

    private const string _substringMarker = "Score: ";
    private string _zeroSequence;
    private string _nonZeroSequence;

    private string ExtractedText => _scoreText.text.Substring(_substringMarker.Length);




    private void Awake()
    {
        InitializeScoreText();
    }

    /// <summary>
    /// Initializes the score text by setting the initial value and color.
    /// </summary>
    private void InitializeScoreText()
    {
        _scoreText.text = $"{_substringMarker}{GlobalFunctions.TextWithColorCode("#BAB7B7", ExtractedText)}";
    }

    /// <summary>
    /// Updates the score text with the specified score value.
    /// </summary>
    /// <param name="score">The new score value.</param>
    public void UpdateScoreText(int score)
    {
        _scoreText.text = $"{_substringMarker}{Converter.DecimalString(score, 8)}";
        _zeroSequence = "";

        for (int i = 0; i < ExtractedText.Length; i++)
        {
            if (!ExtractedText[i].Equals('0'))
            {
                break;
            }

            _zeroSequence += ExtractedText[i];
        }

        _nonZeroSequence = ExtractedText.Substring(_zeroSequence.Length);
        _scoreText.text = $"{_substringMarker}{GlobalFunctions.TextWithColorCode("#BAB7B7", _zeroSequence)}{GlobalFunctions.TextWithColorCode("#FFFFFF", _nonZeroSequence)}";
    }
}
