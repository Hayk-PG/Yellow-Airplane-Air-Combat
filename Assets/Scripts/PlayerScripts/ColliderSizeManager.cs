using System.Collections;
using UnityEngine;

public class ColliderSizeManager : MonoBehaviour
{
    [Header("Collider Component")]
    [SerializeField] private BoxCollider2D _boxCollider;

    [Header("Sprite Renderer Component")]
    [SerializeField] private SpriteRenderer _spriteRenderer;

    [Header("Sprite Renderer Animation Manager Component")]
    [SerializeField] private SpriteRendererAnimationManager _spriteRendererAnimationManager;

    private Vector2 ColliderSize
    {
        get => _boxCollider.size;
        set => _boxCollider.size = value;
    }
    private Vector2 SpriteRendererSize => _spriteRenderer.bounds.size;




    private void OnEnable()
    {
        _spriteRendererAnimationManager.OnAnimationFrameChange += OnSpriteRendererAnimationChange;
    }

    private void OnSpriteRendererAnimationChange()
    {
        StartCoroutine(ExecuteAfterDelay());
    }

    private IEnumerator ExecuteAfterDelay()
    {
        yield return new WaitForSeconds(0.1f);

        MatchColliderSizeToSpriteRendererSize();
    }

    // Matches the size of the collider to the size of the sprite renderer, taking into account the object's rotation.
    private void MatchColliderSizeToSpriteRendererSize()
    {
        float zAngle = transform.rotation.eulerAngles.z;
        float xSize = SpriteRendererSize.x;
        float ySize = SpriteRendererSize.y;

        // Swap x and y sizes if the object is rotated vertically or horizontally
        if (zAngle < 280f && zAngle > 260f || zAngle > 70f && zAngle < 110f)
        {
            xSize = SpriteRendererSize.y;
            ySize = SpriteRendererSize.x;
        }

        ColliderSize = new Vector2(xSize, ySize);
    }
}