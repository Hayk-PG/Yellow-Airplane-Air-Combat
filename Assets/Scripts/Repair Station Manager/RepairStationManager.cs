using System.Collections;
using UnityEngine;
using Pautik;

public class RepairStationManager : MonoBehaviour
{
    [Header("Animator Component")]
    [SerializeField] private Animator _animator;

    private IHealth _repairTarget;
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
        // Sets the repair target and triggers the repair starter if conditions are met.
        _repairTarget = PotentialRepairTarget(collision.gameObject);

        if(_repairTarget == null || _repairStarterCoroutine != null)
        {
            return;
        }

        TriggerRepairStarter();
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
            DisplayRepairTime(timeRemaining);
            yield return new WaitForSeconds(1f);
        }

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
    /// Displays the remaining repair time on the repair station UI.
    /// </summary>
    /// <param name="timeRemaining">The remaining time in seconds.</param>
    private void DisplayRepairTime(int timeRemaining)
    {
        if (!_canRepair)
        {
            Reference.Manager.RepairStationUI.RunTimer(timeRemaining, false);
            return;
        }

        Reference.Manager.RepairStationUI.RunTimer(timeRemaining);
    }

    /// <summary>
    /// Retrieves the potential repair target from the specified GameObject.
    /// </summary>
    /// <param name="gameObject">The GameObject to retrieve the repair target from.</param>
    /// <returns>The IHealth interface of the repair target, or null if it does not exist.</returns>
    private IHealth PotentialRepairTarget(GameObject gameObject)
    {
        return Get<IHealth>.From(gameObject);
    }

    private void PlayRotationAnimation(bool play)
    {
        _animator.SetBool(_rotationAnimationStateName, play);
    }

    private void TriggerRepairStationEnterAnimation()
    {
        _animator.Play(_repairStationEnterAnimationStateName, 0, 0);
    }

    /// <summary>
    /// Destroys the repair station GameObject.
    /// </summary>
    private void DestroyRepairStation()
    {
        Destroy(gameObject);
    }
}