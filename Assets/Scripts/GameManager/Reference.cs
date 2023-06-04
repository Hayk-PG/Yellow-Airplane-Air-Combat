using UnityEngine;

public class Reference : MonoBehaviour
{
    public static Reference Manager { get; private set; }

    [Header("Game Manager")]
    [SerializeField] private AISpawner _enemySpawner;
    [SerializeField] private MapBounds _mapBounds;

    [Header("HUD")]
    [SerializeField] private InputController _inputController;
    [SerializeField] private ShootTargetUI _shootTargetUI;
    [SerializeField] private UIScoreManager _uiScoreManager;

    [Header("Camera")]
    [SerializeField] private CameraSizeController _cameraSizeController;

    //Game Manager
    public AISpawner EnemySpawner => Manager._enemySpawner;
    public MapBounds MapBounds => Manager._mapBounds;

    // HUD
    public InputController InputController => Manager._inputController;
    public ShootTargetUI ShootTargetUI => Manager._shootTargetUI;
    public UIScoreManager UIScoreManager => Manager._uiScoreManager;

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
