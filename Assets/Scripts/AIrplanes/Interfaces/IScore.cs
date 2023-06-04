/// <summary>
/// Interface for entities that have a score.
/// </summary>
public interface IScore 
{
    /// <summary>
    /// The current score value.
    /// </summary>
    int Score { get; set; }

    /// <summary>
    /// Updates the score by the specified value.
    /// </summary>
    /// <param name="value">The value to update the score by.</param>
    void UpdateScore(int value);
}
