using UnityEngine;
using TMPro;
using Pautik;
using System.Collections;

public class UIScoreManager : MonoBehaviour
{
    [Header("Tmp Text Component")]
    [SerializeField] private TMP_Text _scoreText;

    private IEnumerator _coroutine;

    private const string _substringMarker = "Score: ";
    private const string _greyColorHex = "#BAB7B7";
    private const string _whiteColorHex = "#FFFFFF";
    private const string _transparentHex = "#66000000";
    private string _tempColorHex;
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
        TryRunCoroutine();
    }

    /// <summary>
    /// Tries to run the coroutine for animating the score text if it is not already running.
    /// </summary>
    private void TryRunCoroutine()
    {
        if (_coroutine == null)
        {
            _coroutine = AnimateScoreText();
            StartCoroutine(_coroutine);
        }
    }

    /// <summary>
    /// Animates the score text by changing the color with a fading effect.
    /// </summary>
    private IEnumerator AnimateScoreText()
    {
        float threshold = 0.1f;
        float elapsedTime = 0f;
        bool shouldBeTransparent = false;

        while (threshold < 0.6f)
        {
            elapsedTime += Time.deltaTime;

            if(elapsedTime >= threshold)
            {
                if (shouldBeTransparent)
                {
                    _tempColorHex = _transparentHex;
                }
                else
                {
                    _tempColorHex = _whiteColorHex;
                }

                _scoreText.text = $"{_substringMarker}{GlobalFunctions.TextWithColorCode(_greyColorHex, _zeroSequence)}{GlobalFunctions.TextWithColorCode(_tempColorHex, _nonZeroSequence)}";

                shouldBeTransparent = !shouldBeTransparent;
                threshold += 0.1f;
            }

            yield return null;
        }

        _coroutine = null;
    }
}