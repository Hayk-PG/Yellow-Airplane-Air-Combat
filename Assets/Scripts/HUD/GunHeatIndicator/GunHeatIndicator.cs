using System.Collections;
using UnityEngine;
using Pautik;

public class GunHeatIndicator : MonoBehaviour
{
    [Header("Canvas Group")]
    [SerializeField] private CanvasGroup _canvasGroup;

    private IEnumerator _coroutine;
    private float _defaultFireRate;
    private float _currentRate;




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
        GetDefaultFireRate(gameEventType, data);
        HandleGunfire(gameEventType, data);
    }

    private void GetDefaultFireRate(GameEventType gameEventType, object[] data)
    {
        if (gameEventType != GameEventType.OnGunfireDefaultRateInit)
        {
            return;
        }

        _defaultFireRate = (float)data[0];
    }

    private void HandleGunfire(GameEventType gameEventType, object[] data)
    {
        if (gameEventType != GameEventType.OnGunFire)
        {
            return;
        }

        FadeCanvasGroup((float)data[0]);
        TryStartAnimation();
    }

    private void FadeCanvasGroup(float currentRate)
    {
        _currentRate = currentRate;
        _canvasGroup.alpha = Mathf.InverseLerp(_defaultFireRate, 100, _currentRate);
    }

    private void TryStartAnimation()
    {
        if (_coroutine != null)
        {
            return;
        }

        _coroutine = AnimateGunHeatIndicator();
        StartCoroutine(_coroutine);
    }

    private IEnumerator AnimateGunHeatIndicator()
    {      
        bool isScalingUp = true;
        float x = 0f;
        float y = 0f;

        while (_currentRate < (_defaultFireRate / 2f))
        {
            Conditions<bool>.Compare(isScalingUp, delegate 
            {
                x += 5 * Time.deltaTime;
                y += 5 * Time.deltaTime;
            },
            delegate 
            {
                x -= 5 * Time.deltaTime;
                y -= 5 * Time.deltaTime;
            });

            if (x >= 1.3f)
            {
                isScalingUp = false;
            }
            else if (x <= 0f)
            {
                isScalingUp = true;
            }

            transform.localScale = new Vector2(x, y);
            yield return null;
        }

        transform.localScale = new Vector2(1f, 1f);
        _coroutine = null;
    }
}