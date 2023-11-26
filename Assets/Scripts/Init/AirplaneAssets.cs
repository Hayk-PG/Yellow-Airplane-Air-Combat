using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class AirplaneAssets : MonoBehaviour
{
    public static AirplaneAssets Loader { get; protected set; }

    [Header("Asset Reference")]
    [SerializeField] private AssetReference[] _assetReferences;

    public GameObject[] Gameobjects { get; private set; }



    protected virtual void Start()
    {
        CreateInstance();
        StartCoroutine(Initialize());
    }

    private void CreateInstance()
    {
        if (Loader == null)
        {
            Loader = this;
            DontDestroyOnLoad(gameObject);
            return;
        }

        Destroy(gameObject);
    }

    private IEnumerator Initialize()
    {
        Gameobjects = new GameObject[_assetReferences.Length];

        for (int i = 0; i < _assetReferences.Length; i++)
        {
            yield return StartCoroutine(LoadAssetAsync(i));
        }

        InitCallback();
    }

    private IEnumerator LoadAssetAsync(int index)
    {
        bool isAssetLoaded = false;

        _assetReferences[index].LoadAssetAsync<GameObject>().Completed += asset =>
        {
            isAssetLoaded = true;

            Gameobjects[index] = asset.Result;
        };

        yield return new WaitUntil(() => isAssetLoaded == true);
    }

    private void InitCallback()
    {
        GameEventHandler.RaiseEvent(GameEventType.AirplaneAssetInit);
    }
}