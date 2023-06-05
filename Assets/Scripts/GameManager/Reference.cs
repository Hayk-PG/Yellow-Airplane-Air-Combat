using UnityEngine;

public class Reference : MonoBehaviour
{
    public static Reference Manager { get; private set; }

    [Header("Game Manager")]
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private AISpawnManager _aiSpawnManager;
    [SerializeField] private MapBounds _mapBounds;
    [SerializeField] private PlayerEventSystem _playerEventSystem;

    [Header("HUD")]
    [SerializeField] private InputController _inputController;
    [SerializeField] private ShootTargetUI _shootTargetUI;
    [SerializeField] private UIScoreManager _uiScoreManager;
    [SerializeField] private TargetPointerUI _targetPointerUI;

    [Header("Camera")]
    [SerializeField] private CameraSizeController _cameraSizeController;

    //Game Manager
    public GameManager GameManager => Manager._gameManager;
    public AISpawnManager AISpawnManager => Manager._aiSpawnManager;
    public MapBounds MapBounds => Manager._mapBounds;
    public PlayerEventSystem PlayerEventSystem => Manager._playerEventSystem;

    // HUD
    public InputController InputController => Manager._inputController;
    public ShootTargetUI ShootTargetUI => Manager._shootTargetUI;
    public UIScoreManager UIScoreManager => Manager._uiScoreManager;
    public TargetPointerUI  TargetPointerUI => Manager._targetPointerUI;

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
