using UnityEngine;

public class Reference : MonoBehaviour
{
    public static Reference Manager { get; private set; }

    [Header("Game Manager")]
    [SerializeField] private EnemySpawner _enemySpawner;

    [Header("HUD")]
    [SerializeField] private InputController _inputController;

    [Header("Camera")]
    [SerializeField] private CameraSizeController _cameraSizeController;

    //Game Manager
    public EnemySpawner EnemySpawner => Manager._enemySpawner;

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
