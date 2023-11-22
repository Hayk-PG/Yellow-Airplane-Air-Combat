using System.Collections;
using UnityEngine;

public class SettingsUISizeController : MonoBehaviour
{
    [Header("Rect Transform")]   
    [SerializeField] private RectTransform _maskRTransform;
    [SerializeField] private RectTransform _buttonsGroupRTransform;
    [SerializeField] private RectTransform _blurredBackgroundRTransform;




    private void Start()
    {
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
        Canvas canvas = FindObjectOfType<Canvas>();
        _blurredBackgroundRTransform.sizeDelta = new Vector2(canvas.pixelRect.width, canvas.pixelRect.height);
    }

    private void UpdateMaskSize()
    {
        _maskRTransform.sizeDelta = new Vector2(Screen.width, _buttonsGroupRTransform.sizeDelta.y);
    }

    private void UpdateBlurredBackgroundPosition()
    {
        float maskVerticalPosition = _maskRTransform.anchoredPosition.y;
        float maskHeight = _maskRTransform.sizeDelta.y;
        float vertical = (maskHeight / 2f) - maskVerticalPosition;
        _blurredBackgroundRTransform.anchoredPosition = new Vector2(0, vertical);
    }
}