using UnityEngine;

namespace Pautik
{
    public class CustomScrollRectArrows : MonoBehaviour
    {
        private enum ArrowType { Top, Bottom }
        [SerializeField] private ArrowType _arrowType;

        private CanvasGroup _canvasGroup;
        private CustomScrollRect _customScrollRect;


        private void Awake()
        {
            _canvasGroup = Get<CanvasGroup>.From(gameObject);
            _customScrollRect = Get<CustomScrollRect>.From(gameObject);
        }

        private void OnEnable()
        {
            _customScrollRect.onValueChanged += GetScrollRectValue;
        }

        private void OnDisable()
        {
            _customScrollRect.onValueChanged -= GetScrollRectValue;
        }

        private void GetScrollRectValue(bool isContentOutside, float value)
        {
            if (isContentOutside)
            {
                if (_arrowType == ArrowType.Bottom)
                {
                    if (value >= .9f)
                        GlobalFunctions.CanvasGroupActivity(_canvasGroup, true);

                    if (value <= .1f)
                        GlobalFunctions.CanvasGroupActivity(_canvasGroup, false);
                }

                if (_arrowType == ArrowType.Top)
                {
                    if (value <= .1f)
                        GlobalFunctions.CanvasGroupActivity(_canvasGroup, true);

                    if (value >= .9f)
                        GlobalFunctions.CanvasGroupActivity(_canvasGroup, false);
                }

                if (value < 0.9f && value > 0.1f)
                    GlobalFunctions.CanvasGroupActivity(_canvasGroup, true);
            }
        }
    }
}
