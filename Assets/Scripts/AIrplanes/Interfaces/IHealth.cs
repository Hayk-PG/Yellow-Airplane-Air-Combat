/// <summary>
/// Interface representing an object's health.
/// </summary>
public interface IHealth 
{
    /// <summary>
    /// The current health value.
    /// </summary>
    int Health { get; set; }

    /// <summary>
    /// Repairs the object by the specified amount.
    /// </summary>
    /// <param name="repairAmount">The amount of health to repair.</param>
    void Repair(int repairAmount);
}
