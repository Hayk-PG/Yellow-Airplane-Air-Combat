using UnityEngine;

public class CameraDamageEffectHandler : MonoBehaviour
{
    [Header("Animator Component")]
    [SerializeField] private Animator _animator;

    private const string _animationClipName = "Camera Damage Anim";




    private void OnEnable()
    {
        Reference.Manager.PlayerEventSystem.OnPlayerEventTrigger += OnPlayerEventTrigger;
    }

    /// <summary>
    /// Handles the player event trigger and toggles the camera damage effect based on the event type.
    /// </summary>
    /// <param name="playerEventType">The type of player event triggered.</param>
    /// <param name="data">Additional data associated with the event.</param>
    private void OnPlayerEventTrigger(PlayerEventType playerEventType, object[] data)
    {
        if(playerEventType != PlayerEventType.ToggleCameraDamageEffect)
        {
            return;
        }

        ToggleCameraEffect();
    }

    /// <summary>
    /// Toggles the camera damage effect by playing the designated animation clip on the animator component.
    /// </summary>
    private void ToggleCameraEffect()
    {
        _animator.Play(_animationClipName);
    }
}