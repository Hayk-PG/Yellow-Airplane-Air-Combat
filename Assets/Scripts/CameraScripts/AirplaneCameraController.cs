using UnityEngine;

public class AirplaneCameraController : MonoBehaviour
{
    private MovementController _airplane;




    private void Awake()
    {
        _airplane = FindObjectOfType<MovementController>();
    }

    private void Update()
    {
        TrackPlayerAirplane();
    }

    private void TrackPlayerAirplane()
    {
        if (_airplane == null)
        {
            return;
        }

        Vector3 targetPosition = Vector2.Lerp(transform.position, _airplane.Rigidbody.position, 5 * Time.deltaTime);
        targetPosition.x = Mathf.Clamp(targetPosition.x, Reference.Manager.MapBounds.Min.x, Reference.Manager.MapBounds.Max.x);
        targetPosition.y = Mathf.Clamp(targetPosition.y, Reference.Manager.MapBounds.Min.y, Reference.Manager.MapBounds.Max.y);
        targetPosition.z = -10;
        transform.position = targetPosition;
    }
}