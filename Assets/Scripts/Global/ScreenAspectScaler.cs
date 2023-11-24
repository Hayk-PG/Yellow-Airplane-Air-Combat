using UnityEngine;
using Pautik;

public class ScreenAspectScaler : MonoBehaviour
{
    private float _targetAspectRatio = 1280f / 720f;
    private float _defaultSize = 1f;




    private void Start()
    {
        Converter.ScaleDownToTargetAspect(transform, _targetAspectRatio, _defaultSize);
    }
}