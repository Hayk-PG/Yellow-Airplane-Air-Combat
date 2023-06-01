using System.Collections;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    [SerializeField] private ShakeableParticleSystems _muzzleFlash; // Reference to the ShakeableParticleSystems component for muzzle flash

    private float _fireRate = 650f; // Rate of fire in shots per minute

    private bool _isShooting; // Flag indicating if the player is shooting or not

    private IEnumerator _fireRoutine;




    private void OnEnable()
    {
        Reference.Manager.InputController.OnInputController += OnInputController;
    }

    private void OnInputController(InputController.InputType inputType, object[] data)
    {
        Shoot(inputType, data);
    }

    // Handles the shooting logic based on the input type and data.
    private void Shoot(InputController.InputType inputType, object[] data) 
    {
        if(inputType != InputController.InputType.PressShootButton)
        {
            return;
        }

        _isShooting = (bool)data[1];
        TryRunCoroutine();
    }

    // Tries to run the fire coroutine if it is not already running.
    private void TryRunCoroutine()
    {
        // Check if the coroutine is already running
        if (_fireRoutine == null)
        {
            // Create and start the coroutine
            _fireRoutine = FireRoutine(_isShooting);
            StartCoroutine(_fireRoutine);
        }
    }

    // Coroutine for the firing routine.
    private IEnumerator FireRoutine(bool isShooting)
    {
        // Calculate the time between shots based on the fire rate
        float elapsedTime = 60f / _fireRate;

        while (_isShooting)
        {
            ToggleMuzzleFlash();
            PlaySoundEffect();

            yield return new WaitForSeconds(elapsedTime);
        }

        _fireRoutine = null;
    }

    // Toggles the muzzle flash effect.
    private void ToggleMuzzleFlash()
    {
        _muzzleFlash.Play();
    }

    // Plays the sound effect using the ExplosionsSoundController.
    private void PlaySoundEffect()
    {
        ExplosionsSoundController.PlaySound(0, 0);
    }
} 