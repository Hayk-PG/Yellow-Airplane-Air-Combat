using UnityEngine;

/// <summary>
/// Manages the health of an entity and handles damage and healing.
/// </summary>
public class BaseHealthManager : MonoBehaviour, IHealth, IDamage
{
    [Header("Components")]
    [SerializeField] protected ImpactManager _impactManager;
    [SerializeField] protected ShakeableParticleSystems _explosion;
    [SerializeField] protected ParticleSystem _fireTrail;

    [Header("Health")]
    [SerializeField] protected int _health = 100;

    public virtual int Health
    {
        get => _health;
        set => _health = value;
    }




    public virtual void Repair(int repairAmount)
    {
        IncreaseHealth(repairAmount);
        SetFireTrailActive();
    }

    public virtual void DealDamage(int damage, IScore attackerScore = default)
    {
        DecreaseHealth(damage);
        PlayImpactSoundEffect();
        SetFireTrailActive();
        ExplodeAndDestroy(_explosion);
    }

    public void VisualizeHit(Vector2 position)
    {
        _impactManager?.PlayHitParticle(position);
    }

    /// <summary>
    /// Plays the impact sound effect using the specified list index and clip index.
    /// </summary>
    protected virtual void PlayImpactSoundEffect()
    {
        int randomClipIndex = Random.Range(0, ExplosionsSoundController.Clips[2]._clips.Length);
        ExplosionsSoundController.PlaySound(2, randomClipIndex);
    }

    /// <summary>
    /// Sets the active state of the fire trail particle system based on the current health.
    /// </summary>
    protected virtual void SetFireTrailActive()
    {
        bool playParticle = _health <= 50;

        if (_fireTrail.isPlaying == playParticle)
        {
            return;
        }

        if (playParticle)
        {
            _fireTrail.Play(true);
        }
        else
        {
            _fireTrail.Stop(true);
        }
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
    protected virtual void PlayExplosionSoundEffect()
    {
        ExplosionsSoundController.PlaySound(1, 0);
    }

    /// <summary>
    /// Increases the kill score for the attacking player.
    /// </summary>
    /// <param name="scoreAttacker">The player who scored the kill.</param>
    protected virtual void IncrementKillScore(IScore scoreAttacker)
    {
        if (_health > 0)
        {
            return;
        }

        scoreAttacker?.UpdateScore(100);
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