using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;

public abstract class BaseSpriteReferenceLoader<T> : MonoBehaviour
{
    public static T Loader { get; protected set; }

    [Header("Asset Reference")]
    [SerializeField] protected AssetReferenceSprite[] _assetReferences;

    public Sprite[] Sprites { get; protected set; }




    protected virtual void Start()
    {
        CreateInstance();
        StartCoroutine(Initialize());
    }

    protected abstract void CreateInstance();

    protected abstract void InitCallback();

    protected virtual IEnumerator Initialize()
    {
        Sprites = new Sprite[_assetReferences.Length];

        for (int i = 0; i < _assetReferences.Length; i++)
        {
            yield return StartCoroutine(LoadAssetAsync(i));
        }

        InitCallback();
    }

    protected virtual IEnumerator LoadAssetAsync(int index)
    {
        bool isAssetLoaded = false;

        _assetReferences[index].LoadAssetAsync().Completed += asset =>
        {
            isAssetLoaded = true;

            Sprites[index] = asset.Result;
        };

        yield return new WaitUntil(() => isAssetLoaded == true);
    }    
}