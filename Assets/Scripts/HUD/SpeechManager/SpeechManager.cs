using System.Collections;
using UnityEngine;
using TMPro;
using Pautik;

public class SpeechManager : MonoBehaviour
{
    [Header("Canvas Group")]
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private CanvasGroup _engageButtonCanvasGroup;
    [SerializeField] private CanvasGroup _toggleCanvasGroup;

    [Header("Text")]
    [SerializeField] private TMP_Text _lastHopeDefenderText;

    [Header("Button")]
    [SerializeField] private Btn _engageButton;

    private string _lastHopeDefenderMessage = $"Pilot, listen carefully! Our nation is on the edge of destruction, with enemy aircraft closing in on our airspace. You are our last hope, our final line of defense. It's up to you to protect our homeland by taking to the skies and defeating the enemy planes that seek to annihilate everything we hold dear. Your courage and determination hold the key to our survival. May the winds be at your back and luck favor your every move. Good luck, brave pilot!";
    private object[] _data = new object[1];

    private WaitForSecondsRealtime _textDisplayInterval = new WaitForSecondsRealtime(0.06f);
    private WaitForSecondsRealtime _yieldReturnNull = new WaitForSecondsRealtime(0f);
    private WaitForSecondsRealtime _closingDelay = new WaitForSecondsRealtime(0.35f);

    private bool IsLastHopeDefenderMessageEnabled
    {
        get
        {
            return PlayerPrefs.GetInt(Keys.LastHopeDefenderMessageKey, 1) > 0;
        }

        set
        {
            PlayerPrefs.SetInt(Keys.LastHopeDefenderMessageKey, value ? 1 : 0);
        }
    }




    private void OnEnable()
    {
        _engageButton.OnSelect += OnEngageButtonClick;
    }

    private void Start()
    {
        StartCoroutine(DisplayLastHopeDefenderMessage());
    }

    private IEnumerator DisplayLastHopeDefenderMessage()
    {
        if (!IsLastHopeDefenderMessageEnabled)
        {
            yield break;
        }
       
        UpdateLastHopeDefenderMessageState(true);

        _lastHopeDefenderText.text = GlobalFunctions.PartiallyTransparentText("Commander: ");

        int currentIteration  = 0; 
        int totalMessageLength  = _lastHopeDefenderMessage.Length;

        SoundController.PlaySound(0, 0, out float length);

        while (currentIteration  < totalMessageLength )
        {
            foreach (var message in _lastHopeDefenderMessage)
            {
                _lastHopeDefenderText.text += message;
                Conditions<bool>.Compare(message != ' ', ()=> Conditions<bool>.Compare(message == '!', () => SoundOverrider.TypeWriterSpace(), () => SoundOverrider.TypeWriter()), null);
                currentIteration ++;
                yield return _textDisplayInterval;
            }
        }

        StartCoroutine(AnimateToggleButtonCanvasGroup());
    }

    private void OnEngageButtonClick()
    {
        _engageButton.IsInteractable = false;
        SoundOverrider.ImpactClick();
        StartCoroutine(CloseWithDelay());
    }

    private IEnumerator CloseWithDelay()
    {
        yield return _closingDelay;
        UpdateLastHopeDefenderMessageState(false);
    }

    private void UpdateLastHopeDefenderMessageState(bool? isActive)
    {
        if (isActive.HasValue)
        {
            SetCanvasGroupActive(_canvasGroup, isActive.Value);
        }

        _data[0] = isActive;       
        GameEventHandler.RaiseEvent(GameEventType.OnLastHopeDefenderMessageActivity, _data);
    }

    private IEnumerator AnimateToggleButtonCanvasGroup()
    {
        yield return _yieldReturnNull;
        UpdateLastHopeDefenderMessageState(null);

        float elapsedTime = 0f;
        float threshold = 1.5f;

        while(elapsedTime < threshold)
        {
            _toggleCanvasGroup.alpha = _engageButtonCanvasGroup.alpha += Time.unscaledDeltaTime;
            elapsedTime += Time.unscaledDeltaTime;
            yield return _yieldReturnNull;
        }

        SetCanvasGroupActive(_engageButtonCanvasGroup, true);
        SetCanvasGroupActive(_toggleCanvasGroup, true);
    }

    private void SetCanvasGroupActive(CanvasGroup canvasGroup, bool isActive)
    {
        if (canvasGroup.interactable == isActive)
        {
            return;
        }

        GlobalFunctions.CanvasGroupActivity(canvasGroup, isActive);
    }
}