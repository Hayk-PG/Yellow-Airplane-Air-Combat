using UnityEngine;
using TMPro;
using Pautik;

public class RepairStationUI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private TMP_Text _timerText;




    /// <summary>
    /// Runs the timer with the specified time and sets the UI element active.
    /// </summary>
    /// <param name="time">The time value for the timer.</param>
    /// <param name="isActive">Flag indicating whether the UI element should be active.</param>
    public void RunTimer(float time, bool isActive = true)
    {
        GlobalFunctions.CanvasGroupActivity(_canvasGroup, isActive);

        if (!isActive)
        {            
            return;
        }

        _timerText.text = Converter.MmSs(time);
    }
}
