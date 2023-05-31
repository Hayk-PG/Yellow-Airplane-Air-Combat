using UnityEngine;
using TMPro;
using System;
using Pautik;


public class BtnTxt : MonoBehaviour
{
    private TMP_Text _txt;

    private Btn _btn;

    [SerializeField] 
    private string _btnTitle;

    [SerializeField]
    private Color _clrPressed;
    private Color _clrReleased;

    public string BtnTitle
    {
        get => _txt.text;
        private set => _txt.text = value;
    }




    private void Awake()
    {
        _txt = Get<TMP_Text>.From(gameObject);

        _btn = Get<Btn>.From(gameObject);

        SetButtonTitle();

        CacheTextDefaultLook();
    }

    private void OnEnable()
    {
        _btn.OnSelect += delegate { ChangeTextColor(_clrPressed); };

        _btn.OnDeselect += delegate { ChangeTextColor(_clrReleased); };
    }

    private void OnDisable()
    {
        _btn.OnSelect -= delegate { ChangeTextColor(_clrPressed); };

        _btn.OnDeselect -= delegate { ChangeTextColor(_clrReleased); };
    }

    private void CacheTextDefaultLook() => _clrReleased = _txt.color;

    private void ChangeTextColor(Color color) => _txt.color = color;

    private void SetButtonTitle() => BtnTitle = String.IsNullOrEmpty(_btnTitle) ? "" : _btnTitle;

    public void SetButtonTitle(string title) => BtnTitle = title;
}
