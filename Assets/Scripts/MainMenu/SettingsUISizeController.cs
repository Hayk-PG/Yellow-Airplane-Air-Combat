using System.Collections;
using UnityEngine;
using Pautik;

public class SettingsUISizeController : MonoBehaviour
{
    [Header("Rect Transform")]   
    [SerializeField] private RectTransform _maskRTransform;
    [SerializeField] private RectTransform _buttonsGroupRTransform;
    [SerializeField] private RectTransform _blurredBackgroundRTransform;

    private RectTransform _canvasRTransform;

    private Vector2 CanvasSize => _canvasRTransform.rect.size;




    private void Start()
    {
        _canvasRTransform = Get<RectTransform>.From(FindObjectOfType<Canvas>().gameObject);
        SetBlurredBackgroundSize();
        StartCoroutine(Test());
    }

    private IEnumerator Test()
    {
        yield return null;
        UpdateMaskSize();
        UpdateBlurredBackgroundPosition();
    }

    private void SetBlurredBackgroundSize()
    {
        _blurredBackgroundRTransform.sizeDelta = CanvasSize;
    }

    private void UpdateMaskSize()
    {
        _maskRTransform.sizeDelta = new Vector2(CanvasSize.x, _buttonsGroupRTransform.sizeDelta.y);
    }

    private void UpdateBlurredBackgroundPosition()
    {
        float maskVerticalPosition = _maskRTransform.anchoredPosition.y;
        float maskHeight = _maskRTransform.sizeDelta.y;
        float vertical = (maskHeight / 2f) - maskVerticalPosition;
        _blurredBackgroundRTransform.anchoredPosition = new Vector2(0, vertical);
    }
}