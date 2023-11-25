using UnityEngine;

public class SpriteRendererColorChanger : MonoBehaviour
{
    [Header("Sprite Renderer Component")]
    [SerializeField] private SpriteRenderer _spriteRenderer;

    [Header("Airplane Random Color")]
    [SerializeField] private Color[] _colors;




    private void Start()
    {
        SetRandomColor();
    }

    private void SetRandomColor()
    {
        int randomColorIndex = Random.Range(0, _colors.Length);
        _spriteRenderer.color = _colors[randomColorIndex];
    }
}
