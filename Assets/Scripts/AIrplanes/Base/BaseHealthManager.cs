using UnityEngine;

/// <summary>
/// Manages the health of an entity and handles damage and healing.
/// </summary>
public class BaseHealthManager : MonoBehaviour, IDamage
{
    [Header("Components")]
    [SerializeField] private ImpactManager _impactManager;

    [Header("Health")]
    [SerializeField] protected int _health = 100;

    /// <summary>
    /// The current health value.
    /// </summary>
    public virtual int Health
    {
        get => _health;
        set => _health = value;
    }




    /// <summary>
    /// Heals the entity by the specified amount.
    /// </summary>
    /// <param name="hp">The amount of health to add.</param>
    public virtual void Heal(int hp)
    {
        IncreaseHealth(hp);
    }

    public virtual void DealDamage(int damage)
    {
        DecreaseHealth(damage);
    }

    public void VisualizeHit(Vector2 position)
    {
        _impactManager?.PlayHitParticle(position);
    }

    /// <summary>
    /// Plays the impact sound effect using the specified list index and clip index.
    /// </summary>
    /// <param name="listIndex">The index of the sound effect list.</param>
    /// <param name="clipIndex">The index of the sound effect clip within the list.</param>
    protected virtual void PlayImpactSoundEffect(int listIndex = 0, int clipIndex = 0)
    {
        ExplosionsSoundController.PlaySound(listIndex, clipIndex);
    }

    /// <summary>
    /// Explodes and destroys the object if the health is zero or below.
    /// </summary>
    /// <param name="shakeableParticleSystems">The ShakeableParticleSystems component for explosion effect.</param>
    protected virtual void ExplodeAndDestroy(ShakeableParticleSystems shakeableParticleSystems)
    {
        if(_health > 0)
        {
            return;
        }

        Explode(shakeableParticleSystems);
        PlayExplosionSoundEffect();
        DestroyObject();
    }

    /// <summary>
    /// Triggers an explosion effect using ShakeableParticleSystems component.
    /// </summary>
    /// <param name="shakeableParticleSystems">The ShakeableParticleSystems component for explosion effect.</param>
    protected virtual void Explode(ShakeableParticleSystems shakeableParticleSystems)
    {
        if(shakeableParticleSystems == null)
        {
            return;
        }

        shakeableParticleSystems.transform.SetParent(null);
        shakeableParticleSystems.gameObject.SetActive(true);
        shakeableParticleSystems.Play();
    }

    /// <summary>
    /// Plays the explosion sound effect using the specified list index and clip index.
    /// </summary>
    /// <param name="listIndex">The index of the sound effect list.</param>
    /// <param name="clipIndex">The index of the sound effect clip within the list.</param>
    protected virtual void PlayExplosionSoundEffect(int listIndex = 0, int clipIndex = 0)
    {
        ExplosionsSoundController.PlaySound(listIndex, clipIndex);
    }

    /// <summary>
    /// Destroys the game object.
    /// </summary>
    protected virtual void DestroyObject()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// Increases the health by the specified amount, up to a maximum of 100.
    /// </summary>
    /// <param name="hp">The amount of health to add.</param>
    protected virtual void IncreaseHealth(int hp)
    {
        _health = (_health + hp) < 100 ? _health + hp : 100;
    }

    /// <summary>
    /// Decreases the health by the specified amount, clamped to a minimum of 0.
    /// </summary>
    /// <param name="damage">The amount of damage to subtract from the health.</param>
    protected virtual void DecreaseHealth(int damage)
    {
        _health = (_health - damage) > 0 ? _health - damage : 0;
    }
}