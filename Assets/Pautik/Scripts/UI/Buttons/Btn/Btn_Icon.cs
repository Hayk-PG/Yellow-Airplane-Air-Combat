using System;
using UnityEngine;
using UnityEngine.UI;
using Pautik;

public class Btn_Icon : MonoBehaviour
{
    private Image _imgIcon;
    private Btn _btn;

    [SerializeField] 
    private Sprite _sprtPressed;
    private Sprite _sprtReleased;

    [SerializeField] [Space]
    private Color _clrPressed;
    private Color _clrReleased;

    // ChangeIconHolder is used for public access to ChangeIconSprite
    public Action<Sprite> ChangeIconHolder => ChangeIconSprite;



    private void Awake()
    {
        _imgIcon = Get<Image>.From(gameObject);

        _btn = Get<Btn>.From(gameObject);

        _sprtReleased = _imgIcon.sprite;

        _clrReleased = _imgIcon.color;

        CacheIconDefaultLook();
    }

    private void OnEnable()
    {
        _btn.OnSelect += delegate { ChangeIconLook(_btn._buttonClickType, _sprtPressed, _clrPressed); };

        _btn.OnDeselect += delegate { ChangeIconLook(_btn._buttonClickType, _sprtReleased, _clrReleased); };
    }

    private void OnDisable()
    {
        _btn.OnSelect -= delegate { ChangeIconLook(_btn._buttonClickType, _sprtPressed, _clrPressed); };

        _btn.OnDeselect -= delegate { ChangeIconLook(_btn._buttonClickType, _sprtReleased, _clrReleased); };
    }

    private void CacheIconDefaultLook()
    {
        _sprtReleased = _imgIcon.sprite;

        _clrReleased = _imgIcon.color;
    }

    private void ChangeIconLook(Btn.ButtonClickType buttonClickType, Sprite sprite, Color color)
    {
        switch (buttonClickType)
        {
            case 
            Btn.ButtonClickType.ChangeSprite: 
                ChangeIconSprite(sprite); 
                break;

            case 
            Btn.ButtonClickType.ChangeColor: 
                ChangeIconColor(color); 
                break;

            case 
            Btn.ButtonClickType.Both: 
                ChangeIconSprite(sprite); 
                ChangeIconColor(color);
                break;
        }
    }

    // This method is held by ChangeIconHolder delegate for public use only
    private void ChangeIconSprite(Sprite sprite)
    {
        if (sprite == null)
            return;

        _imgIcon.sprite = sprite;
    }

    private void ChangeIconColor(Color color) => _imgIcon.color = color;
}
