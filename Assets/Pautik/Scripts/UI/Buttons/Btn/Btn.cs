using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Pautik;

[RequireComponent(typeof(Button))]

public class Btn : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public enum ButtonClickType { ChangeSprite, ChangeColor, Both, None, OnlyInvokeEvent}
    public ButtonClickType _buttonClickType;

    protected Btn[] _siblings;

    [SerializeField] private Sprite _sprtPressed;
    protected Sprite _sprtReleased;

    [SerializeField] private Color _clrPressed;
    protected Color _clrReleased;

    protected Button Button { get; set; }
    protected Sprite ButtonSprite
    {
        get => Button.image.sprite;
        set => Button.image.sprite = value;
    }
    protected Color ButtonColor
    {
        get => Button.image.color;
        set => Button.image.color = value;
    }

    public bool IsInteractable
    {
        get => Button.interactable;
        set => Button.interactable = value;
    }
    public bool IsSelected { get; private set; }

    public event Action OnSelect;
    public event Action OnDeselect;
    public event Action OnPointerDownHandler;
    public event Action OnPointerUpHandler;
    public event Action OnDragHandler;
    public event Action OnDropHandler;
    public event Action OnBeginDragHandler;
    public event Action OnEndDragHandler;




    protected virtual void Awake()
    {
        GetButton();
        CacheButtonDefaultLook();
    }

    protected virtual void Start()
    {
        GetSiblings();

        Button.onClick.AddListener(Select);
    }

    protected virtual void GetButton()
    {
        Button = Get<Button>.From(gameObject);
    }

    protected virtual void CacheButtonDefaultLook()
    {
        _sprtReleased = Button.image.sprite;
        _clrReleased = Button.image.color;
    }

    protected virtual void GetSiblings()
    {
        _siblings = new Btn[transform.parent.childCount - 1];

        for (int i = 0, j = 0; i < transform.parent.childCount; i++)
        {
            if (Get<Btn>.From(transform.parent.GetChild(i).gameObject) != this)
            {
                _siblings[j] = Get<Btn>.From(transform.parent.GetChild(i).gameObject);
                j++;
            }
        }
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        OnPointerDownHandler?.Invoke();
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        OnPointerUpHandler?.Invoke();
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        OnDragHandler?.Invoke();
    }

    public virtual void OnDrop(PointerEventData eventData)
    {
        OnDropHandler?.Invoke();
    }

    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        OnBeginDragHandler?.Invoke();
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        OnEndDragHandler?.Invoke();
    }

    public virtual void Select()
    {
        if (_buttonClickType == ButtonClickType.None)
            return;

        if (!IsSelected)
        {
            IsSelected = true;
            OnSelect?.Invoke();

            ChangeBuutonLook(_sprtPressed, _clrPressed);
            Conditions<bool>.Compare(_buttonClickType == ButtonClickType.OnlyInvokeEvent, Deselect, DeselectAllSiblings);
        }
    }

    protected virtual void DeselectAllSiblings()
    {
        GlobalFunctions.Loop<Btn>.Foreach(_siblings, sibling =>
        {
            sibling?.Deselect();
        });
    }

    public virtual void Deselect()
    {
        if (IsSelected)
        {
            IsSelected = false;
            OnDeselect?.Invoke();

            if (_sprtReleased == null)
                return;

            ChangeBuutonLook(_sprtReleased, _clrReleased);
        }
    }

    protected virtual void ChangeBuutonLook(Sprite sprite, Color color)
    {
        switch (_buttonClickType)
        {
            case ButtonClickType.ChangeSprite: ChangeSprite(sprite); break;
            case ButtonClickType.ChangeColor: ChangeColor(color); break;
            case ButtonClickType.Both: ChangeBoth(sprite, color); break;
        }
    }

    protected virtual void ChangeSprite(Sprite sprite)
    {
        if (_sprtPressed == null)
            return;

        ButtonSprite = sprite;
    }

    protected virtual void ChangeColor(Color color)
    {
        ButtonColor = color;
    }

    protected virtual void ChangeBoth(Sprite sprite, Color color)
    {
        if (_sprtPressed == null)
            return;

        ButtonSprite = sprite;
        ButtonColor = color;
    }
}
