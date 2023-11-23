using UnityEngine;
using TMPro;

public abstract class BaseUserSignupManager : BaseUserManager
{
    [Header("Canvas Group")]
    [SerializeField] protected CanvasGroup[] _canvasGroups;

    [Header("Input Field")]
    [SerializeField] protected TMP_InputField[] _inputFields;

    [Header("Button")]
    [SerializeField] protected Btn[] _buttons;

    [Header("Toggle")]
    [SerializeField] protected CustomToggle[] _toggles;

    protected bool _isUserNameInputFieldFilled;
    protected bool _isPasswordInputFieldFilled;

    protected virtual string Email => _inputFields[0].text;
    protected virtual string Username => _inputFields[1].text;
    protected virtual string Password => _inputFields[2].text;
    protected bool IsMainButtonInteractable => _isUserNameInputFieldFilled && _isPasswordInputFieldFilled;




    protected override void OnEnable()
    {
        base.OnEnable();

        AddValueChangedListener();

        _buttons[0].OnSelect += OnMainButtonClick;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    protected override void OnGameEvent(GameEventType gameEventType, object[] data)
    {
        HandleRequest(gameEventType, data);
        HandleOperationSuccess(gameEventType, data);
        HandleOperationFailure(gameEventType, data);
    }

    protected void AddValueChangedListener()
    {
        for (int i = 0; i < _inputFields.Length; i++)
        {
            if (_inputFields[i] == null)
            {
                continue;
            }

            switch (i)
            {
                case 0: _inputFields[i].onValueChanged.AddListener(OnEmailInputFieldValueChanged); break;
                case 1: _inputFields[i].onValueChanged.AddListener(OnUserNameInputFieldValueChanged); break;
                case 2: _inputFields[i].onValueChanged.AddListener(OnPasswordInputFieldValueChanged); break;
            }
        }
    }

    protected abstract void HandleRequest(GameEventType gameEventType, object[] data);

    protected abstract void HandleOperationSuccess(GameEventType gameEventType, object[] data);

    protected abstract void HandleOperationFailure(GameEventType gameEventType, object[] data);

    protected virtual void OnEmailInputFieldValueChanged(string text)
    {

    }

    protected virtual void OnUserNameInputFieldValueChanged(string text)
    {
        _isUserNameInputFieldFilled = text.Length >= 3 && text.Length <= 20;
        _buttons[0].IsInteractable = IsMainButtonInteractable;
    }

    protected virtual void OnPasswordInputFieldValueChanged(string text)
    {
        _isPasswordInputFieldFilled = text.Length >= 6 && text.Length <= 25;
        _buttons[0].IsInteractable = IsMainButtonInteractable;
    }

    protected virtual void OnMainButtonClick()
    {
        ToggleCurrentTabInteractability();
    }

    protected void ToggleCurrentTabInteractability()
    {
        _canvasGroups[1].interactable = !_canvasGroups[1].interactable;
    }

    protected virtual void ResetToDefault()
    {
        _buttons[0].Deselect();
        _buttons[0].IsInteractable = false;

        foreach (var inputField in _inputFields)
        {
            inputField.DeactivateInputField();
            inputField.text = "";
        }
    }
}