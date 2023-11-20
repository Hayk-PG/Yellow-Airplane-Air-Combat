using UnityEngine;

public class SoundtrackDisabler : MonoBehaviour
{
    private void Start()
    {
        SoundController.MusicSRCVolume(SoundController.MusicVolume.Down);
    }
}