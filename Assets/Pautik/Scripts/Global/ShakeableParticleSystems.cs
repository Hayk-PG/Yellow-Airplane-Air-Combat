using UnityEngine;
using Pautik;

[RequireComponent(typeof(CameraShaker))]
public class ShakeableParticleSystems : MonoBehaviour
{
    [Header("Particle System Component")]
    [SerializeField] private ParticleSystem[] _particles; // Array of particle systems to be played

    [Header("Camera Shaker Component")]
    [SerializeField] private CameraShaker _cameraShaker; // Reference to the CameraShaker component




    /// <summary>
    /// Plays the particle systems and toggles camera shake.
    /// </summary>
    public void Play()
    {
        GlobalFunctions.Loop<ParticleSystem>.Foreach(_particles, particle => particle.Play(true));

        ToggleCameraShake();
    }

    // Toggles camera shake using the CameraShaker component.
    private void ToggleCameraShake()
    {
        // If CameraShaker component is not assigned, return
        if (_cameraShaker == null) 
            return;

        // Toggle camera shake
        _cameraShaker.ToggleShake();
    }
}
