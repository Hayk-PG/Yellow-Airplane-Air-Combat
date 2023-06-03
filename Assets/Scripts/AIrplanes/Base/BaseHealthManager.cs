using UnityEngine;

public class BaseHealthManager : MonoBehaviour, IDamage
{
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

    /// <summary>
    /// Deals damage to the entity.
    /// </summary>
    /// <param name="damage">The amount of damage to deal.</param>
    public virtual void DealDamage(int damage)
    {
        DecreaseHealth(damage);
    }

    protected virtual void IncreaseHealth(int hp)
    {
        _health = (_health + hp) < 100 ? _health + hp : 100;
    }

    protected virtual void DecreaseHealth(int damage)
    {
        _health = (_health - damage) > 0 ? _health - damage : 0;
    }
}