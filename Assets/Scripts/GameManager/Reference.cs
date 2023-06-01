using UnityEngine;

public class Reference : MonoBehaviour
{
    public static Reference Manager { get; private set; }

    [Header("HUD")]
    [SerializeField] private InputController _inputController;

    [Header("Camera")]
    [SerializeField] private CameraSizeController _cameraSizeController;

    // HUD
    public InputController InputController => Manager._inputController;

    // Camera
    public CameraSizeController CameraSizeController => Manager._cameraSizeController;




    private void Awake()
    {
        CreateInstance();
    }

    private void CreateInstance()
    {
        Manager = this;
    }
}
