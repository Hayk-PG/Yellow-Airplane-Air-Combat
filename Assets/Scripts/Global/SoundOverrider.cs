﻿
public static class SoundOverrider 
{
    public static void UpdateSoundTrackVolume(SoundController.MusicVolume musicVolume, float speed = 1f, bool isMuted = false)
    {
        SoundController.MusicSRCVolume(musicVolume, speed);
        SoundController.ToggleMusicState(isMuted);
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

    public static void TypeWriterSpace()
    {
        UISoundController.PlaySound(2, 25);
    }
}