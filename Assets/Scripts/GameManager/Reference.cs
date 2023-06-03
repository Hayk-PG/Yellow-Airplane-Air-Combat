using UnityEngine;

public class Reference : MonoBehaviour
{
    public static Reference Manager { get; private set; }

    [Header("Game Manager")]
    [SerializeField] private AISpawner _enemySpawner;

    [Header("HUD")]
    [SerializeField] private InputController _inputController;
    [SerializeField] private ShootTargetUI _shootTargetUI;

    [Header("Camera")]
    [SerializeField] private CameraSizeController _cameraSizeController;

    //Game Manager
    public AISpawner EnemySpawner => Manager._enemySpawner;

    // HUD
    public InputController InputController => Manager._inputController;
    public ShootTargetUI ShootTargetUI => Manager._shootTargetUI;

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
