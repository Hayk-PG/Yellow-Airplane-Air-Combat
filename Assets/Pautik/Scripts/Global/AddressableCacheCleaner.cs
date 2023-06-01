using UnityEngine;
using UnityEngine.AddressableAssets;


//ADDRESSABLE
public class AddressableCacheCleaner : MonoBehaviour
{
    private void OnDestroy() => Addressables.ReleaseInstance(gameObject);
}
