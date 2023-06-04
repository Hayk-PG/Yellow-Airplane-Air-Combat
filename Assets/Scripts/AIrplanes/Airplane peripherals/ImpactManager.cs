using UnityEngine;

/// <summary>
/// Manages the impact effects for an entity.
/// </summary>
public class ImpactManager : MonoBehaviour
{
    [SerializeField] private ShakeableParticleSystems[] _hitParticles;



    /// <summary>
    /// Plays the hit particle effect at the specified position.
    /// </summary>
    /// <param name="position">The position to play the hit particle effect.</param>
    public void PlayHitParticle(Vector2 position)
    {
        foreach (var hitParticle in _hitParticles)
        {
            if(hitParticle.transform.parent == transform)
            {
                hitParticle.transform.SetParent(null);
                hitParticle.transform.position = position;
                hitParticle.Play();
                return;
            }
        }
    }
}
