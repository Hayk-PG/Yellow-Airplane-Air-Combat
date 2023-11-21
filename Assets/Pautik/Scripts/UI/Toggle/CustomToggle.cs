using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using Pautik;

public class CustomToggle : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Toggle Component")]
    [SerializeField] private Toggle _toggle; 

    [Header("Tmp Texts")]
    [SerializeField] private TMP_Text _label; 

    [Header("Label")]
    [SerializeField] private string _labelIfOn; 
    [SerializeField] private string _labelIfOff; 
    [SerializeField] private bool _updateLabelOnValueChange; 

    [Header("Sound Index")]
    [SerializeField] private int _listIndex;
    [SerializeField] private int _onClipIndex;
    [SerializeField] private int _offClipIndex;

    private bool _isPointerEntered;

    public event Action<bool> OnValueChange;




    private void Awake()
    {
        UpdateLabel(_updateLabelOnValueChange);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _isPointerEntered = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _isPointerEntered = false;
    }

    public void UpdateToggleState(bool isOn)
    {
        _toggle.isOn = isOn;
        OnValueChange?.Invoke(_toggle.isOn);
        UpdateLabel(_updateLabelOnValueChange);
    }

    public void OnValueChanged()
    {
        OnValueChange?.Invoke(_toggle.isOn);
        Conditions<bool>.Compare(_isPointerEntered, () => PlaySoundEffect(_toggle.isOn ? _onClipIndex : _offClipIndex), null);
        UpdateLabel(_updateLabelOnValueChange);
    }

    private void UpdateLabel(bool updateLabelOnValueChange)
    {
        if (!updateLabelOnValueChange)
        {
            return;
        }

        _label.text = _toggle.isOn ? _labelIfOn : _labelIfOff;
    }

    private void PlaySoundEffect(int clipIndex)
    {
        UISoundController.PlaySound(_listIndex, clipIndex);
    }
}
