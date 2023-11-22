using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.SceneManagement;

public class GoogleInterstitialAdManager  : GoogleAdBaseHandler<GoogleInterstitialAdManager, InterstitialAd, LoadAdError>
{
    protected override string _adUnitId => "ca-app-pub-8791516957988299/3456443093";




    private void Awake()
    {
        CreateInstance(this);
    }

    private void OnEnable()
    {
        GameEventHandler.OnEvent += OnGameEvent;
    }

    private void OnDisable()
    {
        GameEventHandler.OnEvent -= OnGameEvent;
    }

    private void OnGameEvent(GameEventType gameEventType, object[] data)
    {
        HandlePlayerDestroyEvent(gameEventType);
    }

    private void HandlePlayerDestroyEvent(GameEventType gameEventType)
    {
        if (gameEventType != GameEventType.OnPlayerAirplaneDestroy)
        {
            return;
        }

        LoadAndDisplay();
    }

    private void LoadAndDisplay()
    {
        LoadAd();
        ShowAd();
    }

    protected override void LoadAd()
    {
        // Clean up the old ad before loading a new one.
        if (_ad != null)
        {
            _ad.Destroy();
            _ad = null;
        }

        Debug.Log("Loading the interstitial ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest();

        // send the request to load the ad.
        InterstitialAd.Load(_adUnitId, adRequest, HandleAdLoaded);
    }

    protected override void HandleAdLoaded(InterstitialAd interstitialAd, LoadAdError loadAdError)
    {
        // if error is not null, the load request failed.
        if (loadAdError != null || interstitialAd == null)
        {
            Debug.LogError($"interstitial ad failed to load an ad with error: {loadAdError}");
            return;
        }

        Debug.Log($"Interstitial ad loaded with response: {interstitialAd.GetResponseInfo()}");
        _ad = interstitialAd;
    }

    protected override void ShowAd()
    {
        if (_ad != null && _ad.CanShowAd())
        {
            Debug.Log("Showing interstitial ad.");
            _ad.Show();
        }
        else
        {
            Debug.LogError("Interstitial ad is not ready yet.");
        }
    }

    private void RegisterEventHandlers(InterstitialAd interstitialAd)
    {
        // Raised when the ad is estimated to have earned money.
        interstitialAd.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log($"Interstitial ad paid {adValue.Value} {adValue.CurrencyCode}");
        };

        // Raised when an impression is recorded for an ad.
        interstitialAd.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Interstitial ad recorded an impression.");
        };

        // Raised when a click is recorded for an ad.
        interstitialAd.OnAdClicked += () =>
        {
            Debug.Log("Interstitial ad was clicked.");
        };

        // Raised when an ad opened full screen content.
        interstitialAd.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Interstitial ad full screen content opened.");
        };

        // Raised when the ad closed full screen content.
        interstitialAd.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Interstitial ad full screen content closed.");
        };

        // Raised when the ad failed to open full screen content.
        interstitialAd.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError($"Interstitial ad failed to open full screen content with error: {error}");
        };
    }
}