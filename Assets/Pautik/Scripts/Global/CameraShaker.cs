using CartoonFX;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CFXR_Effect))]
public class CameraShaker : MonoBehaviour
{
    [SerializeField] private CFXR_Effect _cfxrEffect; // Reference to the CFXR_Effect component

    private float _shakeDuration; // Duration of the camera shake




    private void Awake()
    {
        DisableCfxrEffect();

        GetShakeDuration();
    }

    /// <summary>
    /// Toggles the camera shake effect.
    /// </summary>
    public void ToggleShake()
    {
        SetCfxrEffectEnabled();

        StartCoroutine(DisableCfxrEffectAfterDelay());
    }

    // Disable the CFXR_Effect component
    private void DisableCfxrEffect()
    {
        _cfxrEffect.enabled = false;
    }

    // Get the duration of the camera shake from the CFXR_Effect component
    private void GetShakeDuration()
    {
        _shakeDuration = _cfxrEffect.cameraShake.duration;
    }

    private IEnumerator DisableCfxrEffectAfterDelay()
    {
        yield return new WaitForSeconds(_shakeDuration);

        // Disable the CFXR_Effect component after the shake duration
        SetCfxrEffectEnabled(false);
    }

    // Enable or disable the CFXR_Effect component based on the provided flag
    private void SetCfxrEffectEnabled(bool isEnabled = true)
    {
        _cfxrEffect.enabled = isEnabled;
    }
}
