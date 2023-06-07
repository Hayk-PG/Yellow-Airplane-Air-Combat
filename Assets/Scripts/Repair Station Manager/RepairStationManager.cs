using System.Collections;
using UnityEngine;
using Pautik;

public class RepairStationManager : MonoBehaviour
{
    [Header("Animator Component")]
    [SerializeField] private Animator _animator;

    private PlayerHealthManager _repairTarget;
    private IEnumerator _repairStarterCoroutine;

    private const string _repairStationEnterAnimationStateName = "Repair Station Enter Anim";
    private const string _rotationAnimationStateName = "rotate";

    private bool _canRepair;




    private void Start()
    {
        PlayRotationAnimation(true);
        StartCoroutine(DestroyRepairStationAfterDelay());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(_repairTarget != null)
        {
            return;
        }

        _repairTarget = PotentialRepairTarget(collision.gameObject);

        bool canStartRepairing = _repairTarget == null || _repairStarterCoroutine != null;

        if (canStartRepairing)
        {
            return;
        }
      
        TriggerRepairStarter();
        DisplayRepairUITimer(true);
        TriggerRepairStationEnterAnimation();
        PlayRotationAnimation(false);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Aborts the repairing process if the same repair target is no longer within the trigger.
        if (_repairTarget != PotentialRepairTarget(collision.gameObject))
        {
            return;
        }

        DisplayRepairUITimer(false);
        AbortRepairing();        
        PlayRotationAnimation(true);
    }

    /// <summary>
    /// Triggers the repair starter coroutine.
    /// </summary>
    private void TriggerRepairStarter()
    {
        _canRepair = true;
        _repairStarterCoroutine = StartRepair();       
        StartCoroutine(_repairStarterCoroutine);
    }

    /// <summary>
    /// Aborts the repairing process.
    /// </summary>
    private void AbortRepairing()
    {
        _repairTarget = null;
        _canRepair = false;       
    }

    /// <summary>
    /// Destroys the repair station after a delay of 60 seconds.
    /// </summary>
    /// <returns>An IEnumerator representing the coroutine.</returns>
    private IEnumerator DestroyRepairStationAfterDelay()
    {
        int timeRemaining = 60;
        int elapsedTime = 1;

        while (timeRemaining > elapsedTime)
        {
            timeRemaining--;
            UpdateRepairUITimer(timeRemaining);
            yield return new WaitForSeconds(1f);
        }

        RunRepairStationSpawnTimerCountdown();
        DisplayRepairUITimer(false);
        DestroyRepairStation();
    }

    /// <summary>
    /// Starts the repairing process.
    /// </summary>
    /// <returns>An IEnumerator representing the coroutine.</returns>
    private IEnumerator StartRepair()
    {
        float time = 0f;
        float elapsedTime = 2f;

        while (_canRepair)
        {
            if(time >= elapsedTime)
            {
                elapsedTime += 0.5f;
                Repair();
                PlayRepairSoundEffect();
            }

            time += Time.deltaTime;
            yield return null;
        }

        _repairStarterCoroutine = null;
    }

    /// <summary>
    /// Repairs the repair target by calling the Repair method on it.
    /// </summary>
    private void Repair()
    {
        _repairTarget?.Repair(1);
    }

    /// <summary>
    /// Plays the repair sound effect.
    /// </summary>
    private void PlayRepairSoundEffect()
    {
        SecondarySoundController.PlaySound(1, 0);
    }

    /// <summary>
    /// Displays or hides the repair UI timer.
    /// </summary>
    /// <param name="isActive">Determines if the timer should be displayed or hidden.</param>
    private void DisplayRepairUITimer(bool isActive)
    {
        if (!_canRepair)
        {
            return;
        }

        Reference.Manager.RepairStationUI.SetActivate(isActive);
    }

    /// <summary>
    /// Updates the repair UI timer with the specified time remaining.
    /// </summary>
    /// <param name="timeRemaining">The time remaining in seconds.</param>
    private void UpdateRepairUITimer(int timeRemaining)
    {
        if (!_canRepair)
        {
            return;
        }

        Reference.Manager.RepairStationUI.RunTimer(timeRemaining);
    }

    /// <summary>
    /// Retrieves the potential repair target from the specified GameObject.
    /// </summary>
    /// <param name="gameObject">The GameObject to retrieve the repair target from.</param>
    /// <returns>The IHealth interface of the repair target, or null if it does not exist.</returns>
    private PlayerHealthManager PotentialRepairTarget(GameObject gameObject)
    {
        return Get<PlayerHealthManager>.From(gameObject);
    }

    /// <summary>
    /// Plays or stops the rotation animation.
    /// </summary>
    /// <param name="play">Determines if the rotation animation should be played or stopped.</param>
    private void PlayRotationAnimation(bool play)
    {
        _animator.SetBool(_rotationAnimationStateName, play);
    }

    /// <summary>
    /// Triggers the repair station enter animation.
    /// </summary>
    private void TriggerRepairStationEnterAnimation()
    {
        _animator.Play(_repairStationEnterAnimationStateName, 0, 0);
    }

    /// <summary>
    /// Runs the repair station spawn timer countdown.
    /// </summary>
    private void RunRepairStationSpawnTimerCountdown()
    {
        Reference.Manager.RepairStationSpawnManager.RunRepairStationSpawnTimerCountdown();
    }

    /// <summary>
    /// Destroys the repair station GameObject.
    /// </summary>
    private void DestroyRepairStation()
    {       
        Destroy(gameObject);
    }
}