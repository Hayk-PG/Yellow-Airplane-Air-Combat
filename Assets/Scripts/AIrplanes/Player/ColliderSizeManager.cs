using System.Collections;
using UnityEngine;

public class ColliderSizeManager : MonoBehaviour
{
    [Header("Collider Component")]
    [SerializeField] private BoxCollider2D _boxCollider;

    private Vector2 ColliderSize
    {
        get => _boxCollider.size;
        set => _boxCollider.size = value;
    }
    private Vector2 SpriteRendererSize { get; set; }




    private void OnEnable()
    {
        GameEventHandler.OnEvent += OnGameEvent;
    }

    private void OnDisable()
    {
        GameEventHandler.OnEvent -= OnGameEvent;
    }

    private void OnGameEvent(GameEventType gameEventType, object[] data)
    {
        HandleRendererCreation(gameEventType, data);
        HandleFrameChange(gameEventType, data);
    }

    private void HandleRendererCreation(GameEventType gameEventType, object[] data)
    {
        if (gameEventType != GameEventType.AirplaneRendererCreated)
        {
            return;
        }

        if ((Transform)data[0] != transform)
        {
            return;
        }

        SpriteRendererSize = ((SpriteRenderer)data[1]).bounds.size;
    }

    private void HandleFrameChange(GameEventType gameEventType, object[] data)
    {
        if (gameEventType != GameEventType.AirplaneFrameChanged)
        {
            return;
        }

        if ((Transform)data[0] != transform)
        {
            return;
        }

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