using UnityEngine;
using UnityEngine.UI;
using Pautik;

public class ShootTargetUI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private Image _shootTargetIcon;

    [Header("Target Icon Animator Component")]
    [SerializeField] private Animator _animator;

    [Header("Main Camera")]
    [SerializeField] private Camera _camera;

    private const string _animationClipName = "TargetIconShakeAnim";




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
    /// Plays the icon shake effect by triggering the specified animation clip on the animator component.
    /// </summary>
    public void PlayIconShakeEffect()
    {
        _animator.Play(_animationClipName, 0, 0);
    }

    /// <summary>
    /// Deactivates the shoot target UI.
    /// </summary>
    public void Deactivate()
    {
        GlobalFunctions.CanvasGroupActivity(_canvasGroup, false);
    }
}
