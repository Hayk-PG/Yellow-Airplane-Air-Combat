using UnityEngine;

public class AIHealthManager : BaseHealthManager
{
    [Header("Particles")]
    [SerializeField] private ShakeableParticleSystems _explosion;
    [SerializeField] private ParticleSystem _fireTrail;

    [Header("Entity Components")]
    [SerializeField] private AirplaneRemovalController _airplaneRemovalController;




    public override void DealDamage(int damage, IScore attackerScore = default)
    {
        base.DealDamage(damage);
        PlayImpactSoundEffect();
        SetFireTrailActive();
        IncrementKillScore(attackerScore);
        ExplodeAndDestroy(_explosion);
        RemoveFromSpawnerList();
    }

    /// <summary>
    /// Sets the active state of the fire trail particle system based on the current health.
    /// </summary>
    private void SetFireTrailActive()
    {
        bool playParticle = _health <= 50;

        if (_fireTrail.isPlaying == playParticle)
        {
            return;
        }

        if(playParticle)
        {
            _fireTrail.Play(true);
        }
        else
        {
            _fireTrail.Stop(true);
        }
    }

    /// <summary>
    /// Removes the AI airplane from the spawner list.
    /// </summary>
    private void RemoveFromSpawnerList()
    {
        _airplaneRemovalController.RemoveFromSpawnerList();
    }

    protected override void PlayImpactSoundEffect(int listIndex = 0, int clipIndex = 0)
    {
        int randomClipIndex = Random.Range(0, ExplosionsSoundController.Clips[2]._clips.Length);
        base.PlayImpactSoundEffect(2, randomClipIndex);
    }

    protected override void PlayExplosionSoundEffect(int listIndex = 0, int clipIndex = 0)
    {
        base.PlayExplosionSoundEffect(1, 0);
    }
}