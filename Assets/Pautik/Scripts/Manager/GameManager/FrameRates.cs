using UnityEngine;

namespace Pautik
{
    public class FrameRates : MonoBehaviour
    {
        [SerializeField] private int _frameRate;

        [Range(0, 2)]
        [SerializeField] private int _vSyncCount;


        private void Awake()
        {
            Application.targetFrameRate = _frameRate;
            QualitySettings.vSyncCount = _vSyncCount;
        }
    }
}
