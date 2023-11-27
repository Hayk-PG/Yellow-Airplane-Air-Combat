using UnityEngine;
using Pautik;

public class AirplaneCameraController : MonoBehaviour
{
    [Header("Box Collider")]
    [SerializeField] private BoxCollider2D _boxCollider;

    private Rigidbody2D _airplaneRigidbody;

    private float _desiredY;
    private float _currentY;
    private float _desiredX;
    private float _currentX;




    private void Awake()
    {
        _airplaneRigidbody = Get<Rigidbody2D>.From(FindObjectOfType<MovementController>().gameObject);
    }

    private void Update()
    {
        TrackPlayerAirplane();
    }

    private void TrackPlayerAirplane()
    {
        _currentY = transform.position.y;
        _currentX = transform.position.x;

        ClampVerticalPosition();
        ClampHorizontalPosition();
        UpdateDesiredVerticalPosition(_airplaneRigidbody.position);
        UpdateDesiredHorizontalPosition(_airplaneRigidbody.position);
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
            _desiredY = Mathf.Lerp(_currentY, playerRigidbodyPosition.y, 5 * Time.deltaTime);
        }
    }

    private void UpdateDesiredHorizontalPosition(Vector2 playerRigidbodyPosition)
    {
        bool isPlayerRightOfBoxCenterAndWithinHorizontalBounds = playerRigidbodyPosition.x > _boxCollider.bounds.center.x && _currentX <= Reference.Manager.MapBounds.Max.x;
        bool isPlayerLeftOfBoxCenterAndWithinHorizontalBounds = playerRigidbodyPosition.x < _boxCollider.bounds.center.x && _currentX >= Reference.Manager.MapBounds.Min.x;

        if (isPlayerRightOfBoxCenterAndWithinHorizontalBounds || isPlayerLeftOfBoxCenterAndWithinHorizontalBounds)
        {
            _desiredX = Mathf.Lerp(_currentX, playerRigidbodyPosition.x, 5 * Time.deltaTime);
        }
    }

    private void UpdateCameraPosition()
    {
        transform.position = new Vector3(_desiredX, _desiredY, -10);
    }
}