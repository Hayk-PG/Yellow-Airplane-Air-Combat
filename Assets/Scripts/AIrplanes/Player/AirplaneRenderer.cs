using UnityEngine;
using Pautik;

public class AirplaneRenderer : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    [Header("Particle")]
    [SerializeField] private ShakeableParticleSystems _muzzleFlash;

    private object[] _data = new object[2];




    private void Awake()
    {
        PublishRenderer();
    }

    private void OnEnable()
    {
        GameEventHandler.OnEvent += OnGameEvent;
    }

    private void OnDisable()
    {
        GameEventHandler.OnEvent -= OnGameEvent;
    }

    private void OnGameEvent(GameEventType gameEventType, object[] data)
    {
        HandleAnimation(gameEventType, data);
        HandleColorUpdater(gameEventType, data);
        HandleMuzzleFlashTrigger(gameEventType, data);
    }

    private void PublishRenderer()
    {
        _data[0] = transform.parent;
        _data[1] = _spriteRenderer;
        GameEventHandler.RaiseEvent(GameEventType.AirplaneRendererCreated, _data);
    }

    private void HandleAnimation(GameEventType gameEventType, object[] data)
    {
        if (gameEventType != GameEventType.AnimateAirplane)
        {
            return;
        }

        if ((Transform)data[0] != transform.parent)
        {
            return;
        }

        _animator.SetBool((string)data[1], (bool)data[2]);
    }

    private void HandleColorUpdater(GameEventType gameEventType, object[] data)
    {
        if (gameEventType != GameEventType.UpdateAirplaneColor)
        {
            return;
        }

        if ((Transform)data[0] != transform.parent)
        {
            return;
        }

        _spriteRenderer.color = (Color)data[1];
    }

    private void HandleMuzzleFlashTrigger(GameEventType gameEventType, object[] data)
    {
        if (gameEventType != GameEventType.MuzzleFlashTriggered)
        {
            return;
        }

        if ((Transform)data[0] != transform.parent)
        {
            return;
        }

        _muzzleFlash.Play();
    }

    /// <summary>
    /// Raises the animation frame change event, indicating that the frame of the sprite animation has changed.
    /// </summary>
    public void RaiseAnimationFrameChangeEvent()
    {
        _data[0] = transform.parent;
        GameEventHandler.RaiseEvent(GameEventType.AirplaneFrameChanged, _data);
    }
}