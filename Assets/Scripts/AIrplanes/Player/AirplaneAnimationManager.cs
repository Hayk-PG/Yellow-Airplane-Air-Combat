using UnityEngine;

public class AirplaneAnimationManager : MonoBehaviour
{
    private object[] _data = new object[3];

    private const string _turnRight = "turnRight";
    private const string _turnLeft = "turnLeft";
    private const string _idle = "idle";
    private const string _dodge = "dodge";

    private bool _isIdleAnimationPlayed;
    private bool _isRightTurnAnimationPlayed;
    private bool _isLeftTurnAnimationPlayed;
    private bool _isDodgeAnimationPlayed;




    public void PlayIdleAnimation(bool play)
    {
        if(_isIdleAnimationPlayed == play)
        {
            return;
        }

        _isIdleAnimationPlayed = play;

        AnimateAirplane(_idle, _isIdleAnimationPlayed);
    }

    public void PlayRightTurnAnimation(bool play)
    {
        if(_isRightTurnAnimationPlayed == play)
        {
            return;
        }

        _isRightTurnAnimationPlayed = play;

        AnimateAirplane(_turnRight, _isRightTurnAnimationPlayed);
    }

    public void PlayLeftTurnAnimation(bool play)
    {
        if(_isLeftTurnAnimationPlayed == play)
        {
            return;
        }

        _isLeftTurnAnimationPlayed = play;

        AnimateAirplane(_turnLeft, _isLeftTurnAnimationPlayed);
    }

    public void PlayDodgeAnimation(bool play)
    {
        if (_isDodgeAnimationPlayed == play)
        {
            return;
        }

        _isDodgeAnimationPlayed = play;

        AnimateAirplane(_dodge, _isDodgeAnimationPlayed);
    }

    private void AnimateAirplane(string parameter, bool state)
    {
        _data[0] = transform;
        _data[1] = parameter;
        _data[2] = state;
        GameEventHandler.RaiseEvent(GameEventType.AnimateAirplane, _data);
    }
}
