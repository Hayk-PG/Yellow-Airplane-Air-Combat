
public class PlayerHealthManager : BaseHealthManager
{
    private object[] _data = new object[1];

    public override void DealDamage(int damage, IScore attackerScore = null)
    {
        ToggleCameraDamageEffect();
        UpdateUIHealthbar(damage);
        base.DealDamage(damage, attackerScore);
    }

    /// <summary>
    /// Triggers the camera damage effect event.
    /// </summary>
    private void ToggleCameraDamageEffect()
    {
        Reference.Manager.PlayerEventSystem.TriggerPlayerEvent(PlayerEventType.ToggleCameraDamageEffect);
    }

    /// <summary>
    /// Updates the UI health bar based on the damage received.
    /// </summary>
    /// <param name="damage">The amount of damage received.</param>
    private void UpdateUIHealthbar(int damage)
    {
        _data[0] = damage;
        Reference.Manager.PlayerEventSystem.TriggerPlayerEvent(PlayerEventType.UpdateHealthbar, _data);
    }
}