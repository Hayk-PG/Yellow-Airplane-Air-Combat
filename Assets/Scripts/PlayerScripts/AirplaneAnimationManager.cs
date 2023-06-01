using UnityEngine;

public class AirplaneAnimationManager : MonoBehaviour
{
    [Header("Animator Component")]
    [SerializeField] private Animator _animator;

    private const string _turnRight = "turnRight";
    private const string _turnLeft = "turnLeft";
    private const string _idle = "idle";
    private const string _dodge = "dodge";

    private bool _isIdleAnimationPlayed;
    private bool _isRightTurnAnimationPlayed;
    private bool _isLeftTurnAnimationPlayed;
    private bool _isDodgeAnimationPlayed;




    /// <summary>
    /// Plays the idle animation if the provided boolean value is true.
    /// </summary>
    /// <param name="play">True to play the idle animation, false to stop it.</param>
    public void PlayIdleAnimation(bool play)
    {
        if(_isIdleAnimationPlayed == play)
        {
            return;
        }

        _isIdleAnimationPlayed = play;

        _animator.SetBool(_idle, _isIdleAnimationPlayed);
    }

    /// <summary>
    /// Plays the turn right animation if the provided boolean value is true.
    /// </summary>
    /// <param name="play">True to play the turn right animation, false to stop it.</param>
    public void PlayRightTurnAnimation(bool play)
    {
        if(_isRightTurnAnimationPlayed == play)
        {
            return;
        }

        _isRightTurnAnimationPlayed = play;

        _animator.SetBool(_turnRight, _isRightTurnAnimationPlayed);
    }

    /// <summary>
    /// Plays the turn left animation if the provided boolean value is true.
    /// </summary>
    /// <param name="play">True to play the turn left animation, false to stop it.</param>
    public void PlayLeftTurnAnimation(bool play)
    {
        if(_isLeftTurnAnimationPlayed == play)
        {
            return;
        }

        _isLeftTurnAnimationPlayed = play;

        _animator.SetBool(_turnLeft, _isLeftTurnAnimationPlayed);
    }

    /// <summary>
    /// Plays the dodge animation if the provided boolean value is true.
    /// </summary>
    /// <param name="play">True to play the dodge animation, false to stop it.</param>
    public void PlayDodgeAnimation(bool play)
    {
        if (_isDodgeAnimationPlayed == play)
        {
            return;
        }

        _isDodgeAnimationPlayed = play;

        _animator.SetBool(_dodge, _isDodgeAnimationPlayed);
    }
}
