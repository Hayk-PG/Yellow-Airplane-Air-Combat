using UnityEngine;

public class CameraSizeController  : MonoBehaviour
{
    [Header("Camera Component")]
    [SerializeField] private Camera _camera;

    private float _ortographicDefaultSize;




    private void Awake()
    {
        InitializeOrtographicDefaultSize();
    }

    public void UpdateOrtographicSize(float targetSize, float changeRate)
    {
        _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, targetSize, changeRate * Time.fixedDeltaTime);
    }

    public void ResetOrtographicSizeToDefault(float changeRate = 5f)
    {
        _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, _ortographicDefaultSize, changeRate * Time.fixedDeltaTime);
    }

    private void InitializeOrtographicDefaultSize()
    {
        _ortographicDefaultSize = _camera.orthographicSize;
    }
}
