using UnityEngine;
using UnityEngine.AddressableAssets;
using System;

//ADDRESSABLE
[Serializable]
public class AssetReferenceAudioClip : AssetReferenceT<AudioClip>
{
    public AssetReferenceAudioClip(string guid) : base(guid)
    {

    }
}
