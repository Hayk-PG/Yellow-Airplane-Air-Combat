using UnityEngine;

public class BlurredBannerSizeController : MonoBehaviour
{
    [Header("Rect Transform")]
    [SerializeField] private RectTransform _backgroundRectTransform;
    [SerializeField] private RectTransform _currentRectTransform;




    private void Start()
    {
        _currentRectTransform.sizeDelta = _backgroundRectTransform.rect.size;
    }
}