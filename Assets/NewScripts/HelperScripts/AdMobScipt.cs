using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using GoogleMobileAds.Api;
using UnityEngine.Advertisements;




public class AdMobScipt : MonoBehaviour
{
    public static AdMobScipt instance;

    private string App_ID = "ca-app-pub-8791516957988299~2526241479";
    private string Banner_Ad_ID = "ca-app-pub-8791516957988299/5811692437";
    private string Interstitial_Ad_ID = "ca-app-pub-8791516957988299/3456443093";

    private BannerView bannerView;
    private InterstitialAd interstitial;


    // Start is called before the first frame update
   
    void Awake()
    {

        if (instance != null) {

            Destroy(gameObject);
        }
        else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }




        MobileAds.Initialize(App_ID);

        this.RequestBanner();
        this.RequestInterstitial();

        
    }
  

    private void Start() {

       /* ShowBannerAd();
        ShowInterstitialAd();*/



    }

    public void RequestBanner() {

        // Create a 320x50 banner at the top of the screen.
        this.bannerView = new BannerView(Banner_Ad_ID, AdSize.Banner, AdPosition.Bottom);

        // Called when an ad request has successfully loaded.
        this.bannerView.OnAdLoaded += this.HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.bannerView.OnAdFailedToLoad += this.HandleOnAdFailedToLoad;
        // Called when an ad is clicked.
        this.bannerView.OnAdOpening += this.HandleOnAdOpened;
        // Called when the user returned from the app after an ad click.
        this.bannerView.OnAdClosed += this.HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        this.bannerView.OnAdLeavingApplication += this.HandleOnAdLeavingApplication;

    }

    public void RequestInterstitial() {

        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(Interstitial_Ad_ID);

        // Called when an ad request has successfully loaded.
        this.interstitial.OnAdLoaded += HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Called when an ad is shown.
        this.interstitial.OnAdOpening += HandleOnAdOpened;
        // Called when the ad is closed.
        this.interstitial.OnAdClosed += HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        this.interstitial.OnAdLeavingApplication += HandleOnAdLeavingApplication;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);


    }


    public void ShowBannerAd() {

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        this.bannerView.LoadAd(request);

    }


    public void ShowInterstitialAd() {

        if (this.interstitial.IsLoaded()) {
            this.interstitial.Show();
        }
    }



    // EVENTS AND DELEGATES FOR ADS

    public void HandleOnAdLoaded(object sender, EventArgs args) {
      
      
        print("HandleOnAdLoaded");
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args) {
       
       
        print("HandleOnAdFailedToLoad");
    }

    public void HandleOnAdOpened(object sender, EventArgs args) {
        MonoBehaviour.print("HandleAdOpened event received");
        bannerView.Destroy();
        interstitial.Destroy();
        this.RequestBanner();
        this.RequestInterstitial();

    }

    public void HandleOnAdClosed(object sender, EventArgs args) {
        MonoBehaviour.print("HandleAdClosed event received");
        bannerView.Destroy();
        interstitial.Destroy();
        this.RequestBanner();
        this.RequestInterstitial();

    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args) {
        MonoBehaviour.print("HandleAdLeavingApplication event received");
    }



}
