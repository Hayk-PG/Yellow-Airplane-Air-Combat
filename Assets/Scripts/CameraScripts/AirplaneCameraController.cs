using UnityEngine;
using Pautik;

public class AirplaneCameraController : MonoBehaviour
{
    [Header("Box Collider")]
    [SerializeField] private BoxCollider2D _boxCollider;

    private Rigidbody2D _airplaneRigidbody;




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
        Vector3 targetPosition = Vector2.Lerp(transform.position, _airplaneRigidbody.position, 5 * Time.deltaTime);
        targetPosition.x = Mathf.Clamp(targetPosition.x, Reference.Manager.MapBounds.Min.x, Reference.Manager.MapBounds.Max.x);
        targetPosition.y = Mathf.Clamp(targetPosition.y, Reference.Manager.MapBounds.Min.y, Reference.Manager.MapBounds.Max.y);
        targetPosition.z = -10;
        transform.position = targetPosition;
    }
}