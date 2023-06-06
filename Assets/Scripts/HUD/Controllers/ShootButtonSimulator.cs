using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#if UNITY_EDITOR
/// <summary>
/// Simulates the behavior of a shoot button based on the space key input.
/// </summary>
public class ShootButtonSimulator : MonoBehaviour
{
    [Header("Shoot Button")]
    [SerializeField] private Btn _shootButton;

    private PointerEventData _ped;
    private Button _button;




    private void Awake()
    {
        InitializeShootButtonPointerEventData();
    }

    private void Update()
    {
        // Updates the shoot button simulation based on the space key input.
        bool isHoldingSpaceKey = Input.GetKey(KeyCode.Space);

        if (isHoldingSpaceKey)
        {
            _button.OnSubmit(_ped);
            Reference.Manager.InputController.OnShootButtonHold(true);
            return;
        }

        Reference.Manager.InputController.OnShootButtonHold(false);
    }

    /// <summary>
    /// Initializes the PointerEventData required for the shoot button simulation.
    /// </summary>
    private void InitializeShootButtonPointerEventData()
    {
        _ped = new PointerEventData(EventSystem.current);
        _button = _shootButton.GetComponent<Button>();
    }
}
#endif