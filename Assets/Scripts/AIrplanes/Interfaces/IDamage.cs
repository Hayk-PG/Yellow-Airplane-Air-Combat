/// <summary>
/// Interface for entities that can take damage.
/// </summary>
public interface IDamage 
{
    /// <summary>
    /// Deals the specified amount of damage to the entity.
    /// </summary>
    /// <param name="damage">The amount of damage to deal.</param>
    /// <param name="attackerScore">Optional: The scoring information associated with the attacker.</param>
    void DealDamage(int damage, IScore attackerScore = default);

    /// <summary>
    /// Visualizes the hit at the specified position.
    /// </summary>
    /// <param name="position">The position to visualize the hit.</param>
    void VisualizeHit(UnityEngine.Vector2 position);
}
