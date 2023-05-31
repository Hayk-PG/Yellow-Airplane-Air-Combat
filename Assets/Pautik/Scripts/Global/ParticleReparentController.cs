using UnityEngine;

public class ParticleReparentController   : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _particles;

    [SerializeField] [Space]
    private Transform _parent;

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

    private void OnParticleSystemStopped() => ReparentAndResetTransform();

    private void InitializeAutoParent()
    {
        if (_parent != null)
            return;

        _parent = transform.parent;
    }

    private void GetDefaultPosition() => _defaultLocalPosition = transform.localPosition;

    private void GetDefaultScale() => _defaultLocalScale = transform.localScale;

    private void GetDefaultRotation() => _defaultLocalRotation = transform.localRotation;

    private void ReparentAndResetTransform()
    {
        transform.SetParent(_parent);
        transform.localPosition = _defaultLocalPosition;
        transform.localScale = _defaultLocalScale;
        transform.localRotation = _defaultLocalRotation;
    }
}
