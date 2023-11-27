using UnityEngine;
using Pautik;

/// <summary>
/// Abstract base class for managing the sensor behavior of an AI.
/// </summary>
public abstract class BaseAISensorManager  : MonoBehaviour
{
    protected IAIMovementManager _aiMovementManager;

    /// <summary>
    /// Determines whether the AI movement manager is null.
    /// </summary>
    protected bool IsAIMovementManagerNull => _aiMovementManager == null;




    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (!IsAIMovementManagerNull)
        {
            return;
        }

        GetCollidedAIMovementManager(collision.gameObject);
        DetectCollision();
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (_aiMovementManager != CollidedAIMovementManager(collision.gameObject))
        {
            return;
        }

        ReleaseCollidedAIMovementManager();
    }

    /// <summary>
    /// Retrieves the AI movement manager from the collided game object.
    /// </summary>
    /// <param name="gameObject">The game object of the collided AI.</param>
    protected virtual void GetCollidedAIMovementManager(GameObject gameObject)
    {
        _aiMovementManager = CollidedAIMovementManager(gameObject);
    }

    /// <summary>
    /// Releases the reference to the collided AI movement manager.
    /// </summary>
    protected virtual void ReleaseCollidedAIMovementManager()
    {
        _aiMovementManager = null;
    }

    /// <summary>
    /// Retrieves the AI movement manager from the collided game object.
    /// </summary>
    /// <param name="gameObject">The game object of the collided ai.</param>
    /// <returns>The enemy movement manager.</returns>
    protected virtual IAIMovementManager CollidedAIMovementManager(GameObject gameObject)
    {
        return Get<IAIMovementManager>.From(gameObject);
    }

    /// <summary>
    /// Abstract method called when a collision with an AI is detected.
    /// </summary>
    protected abstract void DetectCollision();
}