using UnityEngine;
using TMPro;
using Pautik;

public class UserAccountRecoverHandler : MonoBehaviour
{
    [Header("Canvas Group")]
    [SerializeField] private CanvasGroup[] _canvasGroups;

    [Header("Text")]
    [SerializeField] private TMP_Text _message;

    [Header("Input Field")]
    [SerializeField] private TMP_InputField _inputField;

    [Header("Button")]
    [SerializeField] private Btn _confirmButton;

    private string _requestEmailforPasswordReset = GlobalFunctions.PartiallyTransparentText("Please enter the email address associated with your account. We'll send you instructions to reset your password.");
    private string _invalidEmailAddressError => GlobalFunctions.TextWithColorCode("#FF005B", "Oops! The entered email address doesn't seem to be valid. Please double-check and try again.");

    private string Email => _inputField.text;




    private void OnEnable()
    {
        GameEventHandler.OnEvent += OnGameEvent;
        _confirmButton.OnSelect += OnConfirmButtonClick;
    }

    private void OnDisable()
    {
        GameEventHandler.OnEvent -= OnGameEvent;
    }

    private void OnGameEvent(GameEventType gameEventType, object[] data)
    {
        HandleAccountRecovery(gameEventType);
        HandleAccountRecoverySuccess(gameEventType, data);
        HandleAccountRecoveryFailure(gameEventType, data);
    }

    private void HandleAccountRecovery(GameEventType gameEventType)
    {
        if (gameEventType != GameEventType.RequestAccountRecover)
        {
            return;
        }

        ResetToDefault();
        UpdateMessage(_requestEmailforPasswordReset);
        ToggleCanvasGroupsActivity();       
    }

    private void HandleAccountRecoverySuccess(GameEventType gameEventType, object[] data)
    {
        if (gameEventType != GameEventType.UserAccountRecoverySucceed)
        {
            return;
        }

        ToggleCanvasGroupsActivity();
        GameEventHandler.RaiseEvent(GameEventType.RequestUserLogin);
    }

    private void HandleAccountRecoveryFailure(GameEventType gameEventType, object[] data)
    {
        if (gameEventType != GameEventType.UserAccountRecoveryFailed)
        {
            return;
        }

        UpdateMessage(_invalidEmailAddressError);
        ResetToDefault();
    }

    private void OnConfirmButtonClick()
    {
        new PlayfabAccountRecoverHandler(Email);
    }

    private void ResetToDefault()
    {
        _confirmButton.Deselect();
        _inputField.DeactivateInputField();
        _inputField.text = "";
    }

    private void UpdateMessage(string message)
    {
        _message.text = message;
    }

    private void ToggleCanvasGroupsActivity()
    {
        bool isActive = _canvasGroups[0].interactable;

        foreach (var canvasGroup in _canvasGroups)
        {
            GlobalFunctions.CanvasGroupActivity(canvasGroup, !isActive);
        }
    }
}
