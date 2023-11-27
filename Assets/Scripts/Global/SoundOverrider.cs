
public static class SoundOverrider 
{
    public static void UpdateSoundTrackVolume(SoundController.MusicVolume musicVolume, float speed = 1f, bool isMuted = false)
    {
        SoundController.MusicSRCVolume(musicVolume, speed);
        SoundController.ToggleMusicState(isMuted);
    }

    public static void PopUp()
    {
        UISoundController.PlaySound(1, 0);
    }

    public static void Success()
    {
        UISoundController.PlaySound(4, 0);
    }

    public static void Fail()
    {
        UISoundController.PlaySound(4, 1);
    }
}