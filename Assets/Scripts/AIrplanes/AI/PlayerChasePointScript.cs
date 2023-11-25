using UnityEngine;
using Pautik;

public class PlayerChasePointScript : BaseAISensorManager
{
    private void Awake()
    {
        RetrieveAIMovementManager();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _aiMovementManager.DetectFronCollision(_aiMovementManager.CurrentPosition);
        }
    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        
    }

    /// <summary>
    /// Retrieves the AIMovementManager component from the parent object.
    /// </summary>
    private void RetrieveAIMovementManager()
    {
        _aiMovementManager = Get<AIMovementManager>.From(transform.parent.gameObject);
    }

    protected override void DetectCollision()
    {
        
    }
}