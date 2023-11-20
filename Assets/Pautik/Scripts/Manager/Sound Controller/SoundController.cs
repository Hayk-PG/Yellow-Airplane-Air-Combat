using System;
using UnityEngine;
using Pautik;

[Serializable]
public struct Clips
{
    public AudioClip _clip;

    public string _clipName;

    public int _score;
}

[Serializable]
public struct SoundsList
{
    [Space]
    [Header("Clips")]
    public Clips[] _clips;
}


public class SoundController : MonoBehaviour
{
    public static SoundController Instance { get; private set; }

    public enum MusicVolume { Down, Up }

    [Header("Sounds")]
    [SerializeField] private SoundsList[] _soundsList;

    [Header("Audio Source")]
    [SerializeField] private AudioSource[] _allAudioSources;

    private AudioSource _musicSRC;
    private AudioSource _soundSRC;
    private Animator _musicAnimator;

    public SoundsList[] SoundsList
    {
        get => _soundsList;
    }
    public static bool IsMusicMuted
    {
        get => Instance._musicSRC.mute;
        private set => Instance._musicSRC.mute = value;
    }
    public static bool IsSoundMuted
    {
        get => Instance._allAudioSources[0].mute;
        private set => GlobalFunctions.Loop<AudioSource>.Foreach(Instance._allAudioSources, audioSource => audioSource.mute = value);
    }

    public static event Action<bool> onAudioSourceMute;




    private void Awake()
    {
        CreateInstance();

        _musicSRC = Get<AudioSource>.From(transform.Find("MusicSRC").gameObject);

        _soundSRC = Get<AudioSource>.From(transform.Find("SoundSRC").gameObject);

        _musicAnimator = Get<Animator>.From(_musicSRC.gameObject);
    }

    private void CreateInstance()
    {
        if (Instance == null)
        {
            Instance = this;

            DontDestroyOnLoad(gameObject);

            return;
        }

        Destroy(gameObject);
    }

    public static void ToggleMusicState(bool isMuted)
    {
        IsMusicMuted = isMuted;
        PlayerPrefs.SetInt(Keys.IsMusicOn, IsMusicMuted ? 0 : 1);
    }

    public static void ToggleSoundState(bool isMuted)
    {
        IsSoundMuted = isMuted;
        PlayerPrefs.SetInt(Keys.IsSoundOn, IsSoundMuted ? 0 : 1);
    }

    public static void PlaySound(int soundsListIndex, int clipIndex, out float clipLength)
    {
        clipLength = 0;

        if (soundsListIndex < Instance._soundsList.Length && clipIndex < Instance._soundsList[soundsListIndex]._clips.Length)
        {
            Instance._soundSRC.PlayOneShot(Instance._soundsList[soundsListIndex]._clips[clipIndex]._clip);

            clipLength = Instance._soundsList[soundsListIndex]._clips[clipIndex]._clip.length;
        }
    }

    public static void MusicSRCVolume(MusicVolume musicVolume)
    {
        switch (musicVolume)
        {
            case MusicVolume.Down:

                Instance._musicAnimator.Play("MusicSRCVolumeDownAnim");

                break;

            case MusicVolume.Up:

                Instance._musicAnimator.Play("MusicSRCVolumeUpAnim");

                break;
        }
    }
}
