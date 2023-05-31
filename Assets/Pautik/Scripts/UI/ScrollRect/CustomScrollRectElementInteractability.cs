using UnityEngine;


namespace Pautik
{
    [RequireComponent(typeof(CanvasGroup))]
    public class CustomScrollRectElementInteractability : MonoBehaviour
    {
        private CanvasGroup _canvasGroup;
        private CustomScrollRectElement _customScrollRect;


        private void Awake()
        {
            _canvasGroup = Get<CanvasGroup>.From(gameObject);
            _customScrollRect = Get<CustomScrollRectElement>.From(gameObject);
        }

        private void OnEnable()
        {
            _customScrollRect.onDistance += ControlScrollRectInteractability;
        }

        private void OnDisable()
        {
            _customScrollRect.onDistance -= ControlScrollRectInteractability;
        }

        private void ControlScrollRectInteractability(float distance, float length, int index)
        {
            _canvasGroup.alpha = Mathf.InverseLerp(length * 4, 0, Mathf.Abs(distance));
            _canvasGroup.interactable = _canvasGroup.alpha < 0.5f ? false : true;
        }
    }
}
