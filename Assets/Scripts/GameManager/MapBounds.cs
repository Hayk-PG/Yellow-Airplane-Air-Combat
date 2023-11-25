using UnityEngine;

public class MapBounds : MonoBehaviour
{
    [SerializeField] private Vector2 _minBounds;
    [SerializeField] private Vector2 _maxBounds;

    public Vector2 Min => _minBounds;
    public Vector2 Max => _maxBounds;
}
