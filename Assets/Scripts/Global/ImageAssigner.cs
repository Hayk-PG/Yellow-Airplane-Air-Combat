using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]

public class ImageAssigner : MonoBehaviour
{
    [Header("Image")]
    [SerializeField] protected Image _image;

    [Header("Index")]
    [SerializeField] protected int _assetIndex;




    protected virtual void Awake()
    {
        _image.sprite = Background.Loader.Sprites[_assetIndex];
    }
}