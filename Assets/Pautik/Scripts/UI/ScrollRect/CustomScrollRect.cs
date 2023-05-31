using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Pautik
{
    [RequireComponent(typeof(ScrollRect))]
    public class CustomScrollRect : MonoBehaviour, IReset
    {
        [Header("UI Elements")]
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private RectTransform _content;

        public bool IsContentOutside
        {
            get
            {
                return _content.rect.height - _rectTransform.rect.height >= 50;
            }
        }

        public event Action<bool, float> onValueChanged;




        private void Start()
        {
            AddScrollRectListener();
        }

        private void AddScrollRectListener()
        {
            _scrollRect.onValueChanged.AddListener(OnValueChanged);
        }

        /// <summary>
        /// Event handler for the ScrollRect's value change.
        /// </summary>
        /// <param name="position">The current scroll position.</param>
        public void OnValueChanged(Vector2 position)
        {
            bool isScrollPositionAboveLimit = position.y > 1;
            bool isScrollPositionBelowLimit = position.y < 0;

            if (isScrollPositionAboveLimit)
            {
                _scrollRect.verticalNormalizedPosition = 1;
            }

            if (isScrollPositionBelowLimit)
            {
                _scrollRect.verticalNormalizedPosition = 0;
            }

            onValueChanged?.Invoke(IsContentOutside, position.y);
        }

        /// <summary>
        /// Sets the scroll position to a normalized value.
        /// </summary>
        /// <param name="position">The normalized scroll position.</param>
        public void SetNormalizedPosition(float position)
        {
            StartCoroutine(SetNormalizedPositionCoroutine(position));
        }

        private IEnumerator SetNormalizedPositionCoroutine(float position)
        {
            yield return null;

            _scrollRect.verticalNormalizedPosition = position;
        }

        /// <summary>
        /// Sets the scroll position to the default value.
        /// </summary>
        public void SetDefault()
        {
            SetNormalizedPosition(1);
        }
    }
}
