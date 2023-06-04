using UnityEngine;

public class ParticleReparentController   : MonoBehaviour
{
    [Header("Particle Component")]
    [SerializeField] private ParticleSystem _particles;

    [Header("Parent")]
    [SerializeField] private Transform _parent;

    private ParticleSystem.MainModule _mainModule;
    private Vector3 _defaultLocalPosition;
    private Vector3 _defaultLocalScale;
    private Quaternion _defaultLocalRotation;




    private void Awake()
    {
        _mainModule = _particles.main;
        _mainModule.stopAction = ParticleSystemStopAction.Callback;

        InitializeAutoParent();
    }

    private void Start()
    {
        GetDefaultPosition();
        GetDefaultScale();
        GetDefaultRotation();
    }

    /// <summary>
    /// Callback method called when the particle system has stopped playing.
    /// Reparents the particle system to the specified parent transform and resets its local transform.
    /// </summary>
    private void OnParticleSystemStopped()
    {
        ReparentAndResetTransform();
    }

    /// <summary>
    /// Initializes the auto-parent functionality by assigning the parent transform if it is not already assigned.
    /// </summary>
    private void InitializeAutoParent()
    {
        if (_parent != null)
            return;

        _parent = transform.parent;
    }

    /// <summary>
    /// Retrieves the default local position of the particle system.
    /// </summary>
    private void GetDefaultPosition()
    {
        _defaultLocalPosition = transform.localPosition;
    }

    /// <summary>
    /// Retrieves the default local scale of the particle system.
    /// </summary>
    private void GetDefaultScale()
    {
        _defaultLocalScale = transform.localScale;
    }

    /// <summary>
    /// Retrieves the default local rotation of the particle system.
    /// </summary>
    private void GetDefaultRotation()
    {
        _defaultLocalRotation = transform.localRotation;
    }

    /// <summary>
    /// Reparents the particle system to the specified parent transform and resets its local transform properties.
    /// </summary>
    private void ReparentAndResetTransform()
    {
        DestroyIfParentNull();

        transform.SetParent(_parent);
        transform.localPosition = _defaultLocalPosition;
        transform.localScale = _defaultLocalScale;
        transform.localRotation = _defaultLocalRotation;
    }

    /// <summary>
    /// Destroys the game object if the parent transform is null.
    /// </summary>
    private void DestroyIfParentNull()
    {
        if (_parent == null)
        {
            Destroy(gameObject);
        }
    }
}