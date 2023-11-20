using UnityEngine;

public class SoundtrackDisabler : MonoBehaviour
{
    private void Start()
    {
        SoundOverrider.UpdateSoundTrackVolume(SoundController.MusicVolume.Down);
    }
}