using UnityEngine;
using GoogleMobileAds.Api;

public class GoogleRewardedAdManager : GoogleAdBaseHandler<GoogleRewardedAdManager, RewardedAd, LoadAdError>
{
    protected override string _adUnitId => "ca-app-pub-3940256099942544/5224354917";




    private void Awake()
    {
        CreateInstance(this);
    }

    protected override void LoadAd()
    {
        // Clean up the old ad before loading a new one.
        if (_ad != null)
        {
            _ad.Destroy();
            _ad = null;
        }

        Debug.Log("Loading the rewarded ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest();

        // send the request to load the ad.
        RewardedAd.Load(_adUnitId, adRequest, HandleAdLoaded);
    }

    protected override void HandleAdLoaded(RewardedAd rewardedAd, LoadAdError loadAdError)
    {
        // if error is not null, the load request failed.
        if (loadAdError != null || rewardedAd == null)
        {
            Debug.LogError($"Rewarded ad failed to load an ad with error: {loadAdError}");
            return;
        }

        Debug.Log($"Rewarded ad loaded with response: {rewardedAd.GetResponseInfo()}");
        _ad = rewardedAd;
    }

    protected override void ShowAd()
    {
        if (_ad != null && _ad.CanShowAd())
        {
            _ad.Show(reward => { Debug.Log($"Rewarded ad rewarded the user: {reward.Type} {reward.Amount}"); });
        }
    }

    private void RegisterEventHandlers(RewardedAd ad)
    {
        // Raised when the ad is estimated to have earned money.
        ad.OnAdPaid += adValue =>
        {
            Debug.Log($"Rewarded ad paid {adValue.Value} {adValue.CurrencyCode}");
        };

        // Raised when an impression is recorded for an ad.
        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Rewarded ad recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        ad.OnAdClicked += () =>
        {
            Debug.Log("Rewarded ad was clicked.");
        };
        // Raised when an ad opened full screen content.
        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Rewarded ad full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Rewarded ad full screen content closed.");
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError($"Rewarded ad failed to open full screen content with error {error}");
        };
    }
}