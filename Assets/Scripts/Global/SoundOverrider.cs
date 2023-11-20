
public static class SoundOverrider 
{
    public static void UpdateSoundTrackVolume(SoundController.MusicVolume musicVolume)
    {
        SoundController.MusicSRCVolume(musicVolume);
    }

    public static void QuickClick()
    {
        UISoundController.PlaySound(0, 0);
    }

    public static void ImpactClick()
    {
        UISoundController.PlaySound(0, 1);
    }

    public static void PopUp()
    {
        UISoundController.PlaySound(1, 0);
    }

    public static void TypeWriter()
    {
        int randomSound = UnityEngine.Random.Range(0, 25);
        UISoundController.PlaySound(2, randomSound);
    }

    public static void TypeWriterSpace()
    {
        UISoundController.PlaySound(2, 25);
    }
}