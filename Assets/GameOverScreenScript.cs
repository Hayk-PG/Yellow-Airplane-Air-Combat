using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScreenScript : MonoBehaviour
{
    private void OnEnable() {

        AdMobScipt.instance.ShowInterstitialAd();
        
    }
}
