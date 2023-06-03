/// <summary>
/// Interface for entities that can take damage.
/// </summary>
public interface IDamage 
{
    /// <summary>
    /// Deals the specified amount of damage.
    /// </summary>
    /// <param name="damage">The amount of damage to deal.</param>
    void DealDamage(int damage);

    /// <summary>
    /// Visualizes the hit at the specified position.
    /// </summary>
    /// <param name="position">The position to visualize the hit.</param>
    void VisualizeHit(UnityEngine.Vector2 position);
}
