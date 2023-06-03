using UnityEngine;

public class ParticleLifetimeController  : MonoBehaviour
{
    [Header("Destroy Time")]
    [SerializeField] private float _time;




    private void OnParticleSystemStopped()
    {
        Invoke("DestroyAfterDelay", _time);
    }

    private void DestroyAfterDelay()
    {
        Destroy(gameObject);
    }
}
