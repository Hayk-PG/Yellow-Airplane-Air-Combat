using UnityEngine;

/// <summary>
/// Interface for enemy movement managers.
/// </summary>
public interface IAIMovementManager 
{
    /// <summary>
    /// Indicates whether the AI can chase its target.
    /// </summary>
    bool CanChaseTarget { get; set; }

    /// <summary>
    /// The current position of the AI.
    /// </summary>
    Vector2 CurrentPosition { get; set; }

    /// <summary>
    /// Notifies the AI about a collision with the front side.
    /// </summary>
    /// <param name="collidedObjectPosition">The position of the collided object.</param>
    void DetectFronCollision(Vector2 collidedObjectPosition);

    /// <summary>
    /// Notifies the AI about a collision with the top side.
    /// </summary>
    /// <param name="collidedObjectPosition">The position of the collided object.</param>
    void DetectTopCollision(Vector2 collidedObjectPosition);

    /// <summary>
    /// Notifies the AI about a collision with the bottom side.
    /// </summary>
    /// <param name="collidedObjectPosition">The position of the collided object.</param>
    void DetectBottomCollision(Vector2 collidedObjectPosition);
}
