using System;
using UnityEngine;
using UnityEngine.UI;

namespace Pautik
{
    public class CustomScrollRectElement : MonoBehaviour
    {
        private RectTransform _recTransform;
        private Rect _rect;
        private Image _img;
        private Canvas _canvas;
        private CustomScrollRectHorizontal _customScrollRectTest;

        private Vector2 _newSize;
        private float _width, _height, _distance, _actualHorizontalPosition, _normalizedHorizontalPosition, _scaleMultiplier, _length;
        private int _index;

        public event Action<float, float, int> onDistance;


        private void Awake()
        {
            _recTransform = Get<RectTransform>.From(gameObject);
            _rect = _recTransform.rect;
            _img = Get<Image>.From(gameObject);
            _canvas = Get<Canvas>.From(GameObject.Find("Menu"));
            _customScrollRectTest = Get<CustomScrollRectHorizontal>.From(gameObject);

            _width = _rect.width;
            _height = _rect.height;
            _index = transform.GetSiblingIndex();
        }

        private void Update()
        {
            _distance = _customScrollRectTest.transform.position.x - transform.position.x;
            _actualHorizontalPosition = Mathf.Abs(transform.position.x - (_canvas.pixelRect.width / 2));
            _normalizedHorizontalPosition = Mathf.InverseLerp(_canvas.pixelRect.width / 2, 0, _actualHorizontalPosition);
            _scaleMultiplier = _normalizedHorizontalPosition >= 0.5f ? _normalizedHorizontalPosition + 0.5f : 1;
            _newSize = new Vector2(_width * _scaleMultiplier, _height * _scaleMultiplier);
            _recTransform.sizeDelta = _newSize;
            _length = _width / (2960 / Screen.width);
            onDistance?.Invoke(_distance, _length, _index);
        }
    }
}
