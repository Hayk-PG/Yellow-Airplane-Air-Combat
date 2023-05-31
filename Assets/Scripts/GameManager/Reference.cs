using UnityEngine;

public class Reference : MonoBehaviour
{
    public static Reference Manager { get; private set; }

    [Header("HUD")]
    [SerializeField] private InputController _inputController;

    // HUD
    public InputController InputController => Manager._inputController;




    private void Awake()
    {
        CreateInstance();
    }

    private void CreateInstance()
    {
        Manager = this;
    }
}
