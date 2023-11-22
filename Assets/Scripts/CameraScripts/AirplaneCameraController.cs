using UnityEngine;

public class AirplaneCameraController : MonoBehaviour
{
    [Header("Box Collider")]
    [SerializeField] private BoxCollider2D _boxCollider;

    [Header("Smooth Damp Parameters")]
    [SerializeField] private float _smoothTime;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _deltaTime;
    private float _currentVelocity;
    private float _desiredY;
    private float _currentY;
    private float _desiredX;
    private float _currentX;




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
        TrackPlayerAirplane(gameEventType, data);
    }

    private void TrackPlayerAirplane(GameEventType gameEventType, object[] data)
    {
        if (gameEventType != GameEventType.PlayerMoveBroadcast)
        {
            return;
        }

        Vector2 playerRigidbodyPosition = (Vector2)data[0];
        _currentY = transform.position.y;
        _currentX = transform.position.x;
        ClampVerticalPosition();
        ClampHorizontalPosition();
        UpdateDesiredVerticalPosition(playerRigidbodyPosition);
        UpdateDesiredHorizontalPosition(playerRigidbodyPosition);
        UpdateCameraPosition();
    }

    private void ClampVerticalPosition()
    {
        if (_currentY >= Reference.Manager.MapBounds.Max.y)
        {
            _currentY = Reference.Manager.MapBounds.Max.y;
        }

        else if (_currentY <= Reference.Manager.MapBounds.Min.y)
        {
            _currentY = Reference.Manager.MapBounds.Min.y;
        }
    }

    private void ClampHorizontalPosition()
    {
        if (_currentX >= Reference.Manager.MapBounds.Max.x)
        {
            _currentX = Reference.Manager.MapBounds.Max.x;
        }

        else if (_currentX <= Reference.Manager.MapBounds.Min.x)
        {
            _currentX = Reference.Manager.MapBounds.Min.x;
        }
    }

    private void UpdateDesiredVerticalPosition(Vector2 playerRigidbodyPosition)
    {
        bool isPlayerAboveBoxCenterAndWithinVerticalBounds = playerRigidbodyPosition.y > _boxCollider.bounds.center.y && _currentY <= Reference.Manager.MapBounds.Max.y;
        bool isPlayerBelowBoxCenterAndWithinVerticalBounds = playerRigidbodyPosition.y < _boxCollider.bounds.center.y && _currentY >= Reference.Manager.MapBounds.Min.y;

        if (isPlayerAboveBoxCenterAndWithinVerticalBounds || isPlayerBelowBoxCenterAndWithinVerticalBounds)
        {
            _desiredY = Mathf.SmoothDamp(_currentY, playerRigidbodyPosition.y, ref _currentVelocity, _smoothTime, _maxSpeed, _deltaTime);
        }
    }

    private void UpdateDesiredHorizontalPosition(Vector2 playerRigidbodyPosition)
    {
        bool isPlayerRightOfBoxCenterAndWithinHorizontalBounds = playerRigidbodyPosition.x > _boxCollider.bounds.center.x && _currentX <= Reference.Manager.MapBounds.Max.x;
        bool isPlayerLeftOfBoxCenterAndWithinHorizontalBounds = playerRigidbodyPosition.x < _boxCollider.bounds.center.x && _currentX >= Reference.Manager.MapBounds.Min.x;

        if (isPlayerRightOfBoxCenterAndWithinHorizontalBounds || isPlayerLeftOfBoxCenterAndWithinHorizontalBounds)
        {
            _desiredX = Mathf.SmoothDamp(_currentX, playerRigidbodyPosition.x, ref _currentVelocity, _smoothTime, _maxSpeed, _deltaTime);
        }
    }

    private void UpdateCameraPosition()
    {
        transform.position = new Vector3(_desiredX, _desiredY, -10);
    }
}