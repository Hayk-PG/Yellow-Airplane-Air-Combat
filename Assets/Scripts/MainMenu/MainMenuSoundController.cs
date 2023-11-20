using UnityEngine;

public class MainMenuSoundController : MonoBehaviour
{
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
        if (gameEventType != GameEventType.OnMainMenuInit)
        {
            return;
        }

        SoundOverrider.UpdateSoundTrackVolume(SoundController.MusicVolume.Up);
        SoundController.ToggleMusicState(false);
    }
}