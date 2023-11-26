using UnityEngine;

public class SettingsInputManager : MonoBehaviour
{
    /// <summary>
    /// 0: Resume, 1: Home, 2: Play or Replay, 3: Quit 4: Logout 5: Leaderboard (Not included)
    /// </summary>
    [Header("Button")]
    [SerializeField] internal Btn[] _buttons;




    private void OnEnable()
    {
        _buttons[0].OnSelect += () => GameEventHandler.RaiseEvent(GameEventType.OnResumeButtonClick);
        _buttons[1].OnSelect += () => { GameEventHandler.RaiseEvent(GameEventType.ResetGameSpeed); MyScene.Manager.LoadSceneAsync(0); };
        _buttons[2].OnSelect += () => { GameEventHandler.RaiseEvent(GameEventType.ResetGameSpeed); MyScene.Manager.LoadSceneAsync(1); };
        _buttons[3].OnSelect += () => Application.Quit();
        _buttons[4].OnSelect += () => GameEventHandler.RaiseEvent(GameEventType.RequestUserLogout);
    }
}