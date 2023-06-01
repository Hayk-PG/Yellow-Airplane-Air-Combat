using System;
using UnityEngine;
using Pautik;

public class ExplosionsSoundController : MonoBehaviour
{
    public static ExplosionsSoundController Instance { get; private set; }

    private AudioSource _audioSRC;

    [Serializable]
    public struct ClipsList
    {
        [SerializeField]
        private string _title;

        public AudioClip[] _clips;
    }

    [SerializeField]
    private ClipsList[] _clipsList;

    public static ClipsList[] Clips => Instance._clipsList;



    private void Awake()
    {
        Instance = this;

        _audioSRC = Get<AudioSource>.From(transform.Find("SoundSRC_Explosions").gameObject);
    }

    public static void PlaySound(int listIndex, int clipIndex)
    {
        if (Instance == null)
            Instance = FindObjectOfType<ExplosionsSoundController>();

        if (listIndex >= Instance._clipsList.Length || clipIndex >= Instance._clipsList[listIndex]._clips.Length)
            return;

        Instance._audioSRC.PlayOneShot(Instance._clipsList[listIndex]._clips[clipIndex]);
    }
}
