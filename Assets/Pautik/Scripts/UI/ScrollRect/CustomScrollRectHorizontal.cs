using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Pautik
{
    public class CustomScrollRectHorizontal : MonoBehaviour, IBeginDragHandler, IEndDragHandler
    {
        private ScrollRect _scrollRect;
        private CustomScrollRectElement[] _elements;
        private Transform _content;

        private bool _isDragged;
        private bool _hasElementInfoReceived;
        private int _elementIndex;


        private void Awake()
        {
            _scrollRect = Get<ScrollRect>.From(gameObject);
            _elements = GetComponentsInChildren<CustomScrollRectElement>();
            _content = _scrollRect.content;
        }

        private void Start()
        {
            Set(0.5f);
        }

        private void OnEnable()
        {
            GlobalFunctions.Loop<CustomScrollRectElement>.Foreach(_elements, element => { element.onDistance += GetElementInfo; });
        }

        private void OnDisable()
        {
            GlobalFunctions.Loop<CustomScrollRectElement>.Foreach(_elements, element => { element.onDistance -= GetElementInfo; });
        }

        private void Update()
        {
            _scrollRect.onValueChanged.RemoveAllListeners();
            _scrollRect.onValueChanged.AddListener(Get);

            if (_hasElementInfoReceived)
            {
                float newValue = Mathf.Lerp(_scrollRect.horizontalNormalizedPosition, Mathf.InverseLerp(1, _elements.Length, _elementIndex), 5 * Time.deltaTime);
                Set(newValue);
            }
        }

        private void Get(Vector2 position)
        {

        }

        public void Set(float newValue)
        {
            _scrollRect.horizontalNormalizedPosition = newValue;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _isDragged = true;
            _hasElementInfoReceived = false;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _isDragged = false;
        }

        private void GetNewPosition(float distance)
        {
            float currentHoriz = _content.position.x;
            float newHoriz = currentHoriz += distance;
            Vector2 position = new Vector2(newHoriz, _content.position.y);
        }

        private void GetElementIndex(int index)
        {
            _elementIndex = index;
        }

        private void GetElementInfo(float distance, float length, int index)
        {
            bool isElementNearToTheCenter = distance <= length && distance >= -length;
            bool canReceiveElementInfo = !_isDragged && !_hasElementInfoReceived;

            if (isElementNearToTheCenter)
            {
                if (canReceiveElementInfo)
                {
                    GetNewPosition(distance);
                    GetElementIndex(index);
                    _hasElementInfoReceived = true;
                }
            }
            else
            {

            }
        }
    }
}
