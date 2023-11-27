using UnityEngine;

public abstract class GoogleAdBaseHandler<T, T1, T2> : MonoBehaviour 
{
    public static T Instance { get; protected set; }

    // These ad units are configured to always serve test ads.
    #if UNITY_ANDROID
    protected abstract string _adUnitId { get; }
    #elif UNITY_IPHONE
    protected abstract string _adUnitId { get; }
    #else
    protected abstract string _adUnitId { get; }
    #endif

    protected T1 _ad;




    protected virtual void CreateInstance(T t)
    {
        if (Instance == null)
        {
            Instance = t;
            DontDestroyOnLoad(gameObject);
            return;
        }

        Destroy(gameObject);
    }

    protected abstract void LoadAd();

    protected abstract void HandleAdLoaded(T1 t1, T2 t2);

    protected abstract void ShowAd();
}