using UnityEngine;
using TMPro;
using Pautik;

public class RepairStationUI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private TMP_Text _timerText;

    /// <summary>
    /// Indicates whether the repair station UI is active.
    /// </summary>
    private bool IsActive => _canvasGroup.alpha == 1f;




    /// <summary>
    /// Sets the activation state of the repair station UI.
    /// </summary>
    /// <param name="isActive">The desired activation state. Default is false.</param>
    public void SetActivate(bool isActive = false)
    {
        GlobalFunctions.CanvasGroupActivity(_canvasGroup, isActive);
    }

    /// <summary>
    /// Updates the timer text on the repair station UI.
    /// </summary>
    /// <param name="time">The remaining time to display on the timer.</param>
    public void RunTimer(float time)
    {
        if (!IsActive)
        {
            return;
        }

        _timerText.text = Converter.MmSs(time);
    }
}