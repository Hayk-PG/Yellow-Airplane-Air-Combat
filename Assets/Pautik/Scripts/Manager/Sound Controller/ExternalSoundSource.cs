using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]

public class ExternalSoundSource : MonoBehaviour
{
    public enum SoundControllerType { SoundController, SecondarySoundController, ExplosionSoundController, UiSoundController}
    public enum PlayMode { OnAwake, OnEnable, OnStart}

    [SerializeField]
    private SoundControllerType _soundControllerType;

    [SerializeField] [Space]
    private PlayMode _playMode;

    [SerializeField] [Space]
    private AudioSource _audioSource;

    [SerializeField] [Space]
    private Animator _animator;

    [SerializeField] [Space]
    private int _collectionIndex, _clipIndex;

    [SerializeField] [Space]
    private bool _isDestroyable, _dontPlayAnimation;

    public float Volume
    {
        get => _audioSource.volume;
        set => _audioSource.volume = value;
    }


    


    private void Awake()
    {
        OnMute(SoundController.IsSoundMuted);

        LoadAudioClip();

        Play(PlayMode.OnAwake);
    }

    private void Start() => Play(PlayMode.OnStart);

    private void OnEnable()
    {
        SoundController.onAudioSourceMute += OnMute;

        Play(PlayMode.OnEnable);
    }

    private void OnDisable() => SoundController.onAudioSourceMute -= OnMute;

    private void LoadAudioClip()
    {
        switch (_soundControllerType)
        {
            case SoundControllerType.SoundController:
                
                AssignAudioClip(SoundController.Instance.SoundsList[_collectionIndex]._clips[_clipIndex]._clip);
                break;

            case SoundControllerType.SecondarySoundController:

                AssignAudioClip(SecondarySoundController.Clips[_collectionIndex]._clips[_clipIndex]);
                break;

            case SoundControllerType.ExplosionSoundController:

                AssignAudioClip(ExplosionsSoundController.Clips[_collectionIndex]._clips[_clipIndex]);
                break;

            case SoundControllerType.UiSoundController:

                AssignAudioClip(UISoundController.Clips[_collectionIndex]._clips[_clipIndex]);
                break;
        }
    }

    private void AssignAudioClip(AudioClip audioClip)
    {
        if (audioClip == null)
            return;

        _audioSource.clip = audioClip;
    }

    private void OnMute(bool isMuted) => _audioSource.mute = isMuted;

    public void Play(PlayMode playMode)
    {
        if (playMode == _playMode)
        {
            _audioSource.Play();

            PlayAnimation(true);
        }
    }

    private void PlayAnimation(bool play = false)
    {
        if (_dontPlayAnimation)
            return;

        if (play)
            _animator.SetTrigger("play");
        else
            _animator.SetTrigger("stop");
    }

    public void Stop(bool unparent, bool stop = false)
    {
        PlayAnimation(false);

        if (unparent)
            transform.SetParent(null);

        if (stop)
            _audioSource.Stop();
    }

    public void OnAnimationEnd()
    {
        
    }
}
