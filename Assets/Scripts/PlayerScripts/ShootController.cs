using System.Collections;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    [SerializeField] private ShakeableParticleSystems _muzzleFlash; // Reference to the ShakeableParticleSystems component for muzzle flash

    private float _fireRate = 400f; // Rate of fire in shots per minute

    private bool _isShooting; // Flag indicating if the player is shooting or not





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

        StartCoroutine(FireRoutine());
    }

    // Coroutine for the firing routine.
    private IEnumerator FireRoutine()
    {
        // Calculate the time between shots based on the fire rate
        float elapsedTime = 60f / _fireRate;

        while (_isShooting)
        {
            ToggleMuzzleFlash();

            yield return new WaitForSeconds(elapsedTime);
        }
    }

    // Toggles the muzzle flash effect.
    private void ToggleMuzzleFlash()
    {
        _muzzleFlash.Play();
    }
} 