using UnityEngine;
using UnityEngine.AddressableAssets;

public class MyScene : MonoBehaviour
{
    public static MyScene Manager { get; private set; }

    [Header("Scene Asset")]
    [SerializeField] private AssetReference[] _sceneReferences;






    private void Awake()
    {
        CreateInstance();
    }

    private void CreateInstance()
    {
        if (Manager == null)
        {
            Manager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadSceneAsync(int index)
    {
        _sceneReferences[index].LoadSceneAsync();
    }
}
