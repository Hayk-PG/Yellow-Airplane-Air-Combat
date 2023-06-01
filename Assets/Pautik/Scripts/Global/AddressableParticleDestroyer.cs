using UnityEngine;
using UnityEngine.AddressableAssets;

//ADDRESSABLE
public class AddressableParticleDestroyer : MonoBehaviour
{
    [SerializeField] private enum StopBehaviour { None, Disable, Destroy}
    [SerializeField] private StopBehaviour _stopBehaviour;


    private void OnParticleSystemStopped() => OnStopBehaviour(_stopBehaviour);

    private void OnStopBehaviour(StopBehaviour stopBehaviour)
    {
        switch (stopBehaviour)
        {
            case StopBehaviour.None: return;
            case StopBehaviour.Destroy: DestroyGameObject(); break;
            case StopBehaviour.Disable: DisableGameObject(); break;
        }
    }

    private void DestroyGameObject()
    {
        Addressables.ReleaseInstance(gameObject);
        Destroy(gameObject);
    }

    private void DisableGameObject()
    {
        gameObject.SetActive(false);
    }
}
