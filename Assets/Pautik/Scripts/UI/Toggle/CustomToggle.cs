using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Custom toggle component that updates a label and raises an event when the value changes.
/// </summary>
public class CustomToggle : MonoBehaviour
{
    [Header("Toggle Component")]
    [SerializeField] private Toggle _toggle; // Reference to the Toggle component

    [Header("Tmp Texts")]
    [SerializeField] private TMP_Text _label; // Reference to the TMP_Text component for label display

    [Header("Label")]
    [SerializeField] private string _labelIfOn; // Label text when the toggle is on
    [SerializeField] private string _labelIfOff; // Label text when the toggle is off

    [SerializeField] private bool _updateLabelOnValueChange; // Flag indicating whether to update the label on value change

    public event Action<bool> OnValueChange; // Event raised when the value of the toggle changes




    private void Awake()
    {
        UpdateLabel(_updateLabelOnValueChange);
    }

    // Callback method for the toggle value change event.
    public void OnValueChanged()
    {
        OnValueChange?.Invoke(_toggle.isOn);

        UpdateLabel(_updateLabelOnValueChange);
    }

    // Updates the label based on the toggle value.
    private void UpdateLabel(bool updateLabelOnValueChange)
    {
        if (!updateLabelOnValueChange)
        {
            return;
        }

        _label.text = _toggle.isOn ? _labelIfOn : _labelIfOff;
    }
}
