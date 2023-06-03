using UnityEngine;

public class AIHealthManager : BaseHealthManager
{
    [Header("Explosion Particle")]
    [SerializeField] private ShakeableParticleSystems _explosion;



    public override void DealDamage(int damage)
    {
        base.DealDamage(damage);
        PlayImpactSoundEffect();
        ExplodeAndDestroy(_explosion);
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
