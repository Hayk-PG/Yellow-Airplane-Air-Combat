using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowAdsScript : MonoBehaviour
{
    public static ShowAdsScript instance;
    public int num = 0; // 

    public bool showBannerAd = false;

    private void Awake() {
        
        if(instance != null) {

            Destroy(gameObject);
        }
        else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
       


    }


    private void Update() {

        if (num == 1) {

            showBannerAd = true;
        }

        if (num >= 3) {

            num = 0;
            showBannerAd = false;
        }
    }


    public void ShowBannerAd() {

        if (showBannerAd) {
            AdMobScipt.instance.ShowBannerAd();
            print("Banner Ad is visible");
        }
       
    }

}// CLASS
