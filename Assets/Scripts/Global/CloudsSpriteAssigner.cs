using UnityEngine;

public class CloudsSpriteAssigner : MonoBehaviour
{
    [Header("Sprite Renderer")]
    [SerializeField] private SpriteRenderer[] _spriteRenderers;




    private void Awake()
    {
        for (int i = 0; i < _spriteRenderers.Length; i++)
        {
            switch (i)
            {
                case 0: case 1: _spriteRenderers[i].sprite = Cloud.Loader.Sprites[4]; break;
                case 2: _spriteRenderers[i].sprite = Cloud.Loader.Sprites[0]; break;
                case 3: _spriteRenderers[i].sprite = Cloud.Loader.Sprites[1]; break;
                case 4: case 7: case 10: _spriteRenderers[i].sprite = Cloud.Loader.Sprites[2]; break;
                default: _spriteRenderers[i].sprite = Cloud.Loader.Sprites[3]; break;
            }
        }
    }
}