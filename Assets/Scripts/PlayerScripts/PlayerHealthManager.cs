
public class PlayerHealthManager : BaseHealthManager
{
    private object[] _data = new object[1];




    public override void Repair(int repairAmount)
    {
        UpdateUIHealthbar(PlayerEventType.UpdateHealthbarOnRepair, repairAmount);
        base.Repair(repairAmount);
    }

    public override void DealDamage(int damage, IScore attackerScore = null)
    {
        ToggleCameraDamageEffect();
        UpdateUIHealthbar(PlayerEventType.UpdateHealthbarOnDamage, damage);
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
    /// Updates the UI health bar based on the specified player event type and value.
    /// </summary>
    /// <param name="playerEventType">The type of player event.</param>
    /// <param name="value">The value associated with the player event.</param>
    private void UpdateUIHealthbar(PlayerEventType playerEventType, int value)
    {
        _data[0] = value;
        Reference.Manager.PlayerEventSystem.TriggerPlayerEvent(playerEventType, _data);
    }
}