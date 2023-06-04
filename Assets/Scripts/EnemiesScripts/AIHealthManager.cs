using UnityEngine;

public class AIHealthManager : BaseHealthManager
{
    [Header("Explosion Particle")]
    [SerializeField] private ShakeableParticleSystems _explosion;

    [Header("Entity Components")]
    [SerializeField] private AirplaneRemovalController _airplaneRemovalController;




    public override void DealDamage(int damage, IScore attackerScore = default)
    {
        base.DealDamage(damage);
        PlayImpactSoundEffect();
        IncrementKillScore(attackerScore);
        ExplodeAndDestroy(_explosion);
        RemoveFromSpawnerList();
    }

    /// <summary>
    /// Removes the AI airplane from the spawner list.
    /// </summary>
    protected void RemoveFromSpawnerList()
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