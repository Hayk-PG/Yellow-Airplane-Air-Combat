using UnityEngine;
using GoogleMobileAds.Api;

public class GoogleAdsInitializer : MonoBehaviour
{
    public static GoogleAdsInitializer Instance { get; private set; }




    private void Awake()
    {
        CreateInstance();
    }

    private void Start()
    {
        Initialize();
    }

    private void CreateInstance()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }

        Destroy(gameObject);
    }

    private void Initialize()
    {
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(initStatus => { print("MobileAds SDK is initialized"); });
    }
}