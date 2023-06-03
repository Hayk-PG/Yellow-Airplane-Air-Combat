using UnityEngine;
using UnityEngine.UI;
using Pautik;

public class ShootTargetUI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private Image _shootTargetIcon;

    [Header("Main Camera")]
    [SerializeField] private Camera _camera;




    /// <summary>
    /// Activates the shoot target UI and positions the shoot target icon at the specified world position.
    /// </summary>
    /// <param name="worldPosition">The world position to place the shoot target icon.</param>
    public void Activate(Vector2 worldPosition)
    {
        GlobalFunctions.CanvasGroupActivity(_canvasGroup, true);
        _shootTargetIcon.transform.position = CameraPoint.ScreenPoint(_camera, worldPosition);
    }

    /// <summary>
    /// Deactivates the shoot target UI.
    /// </summary>
    public void Deactivate()
    {
        GlobalFunctions.CanvasGroupActivity(_canvasGroup, false);
    }
}
