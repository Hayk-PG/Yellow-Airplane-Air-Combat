using UnityEngine;

public class SettingsInputManager : MonoBehaviour
{
    /// <summary>
    /// 0: Resume, 1: Home, 2: Play or Replay, 3: Quit
    /// </summary>
    [Header("Button")]
    [SerializeField] internal Btn[] _buttons;




    private void OnEnable()
    {
        _buttons[0].OnSelect += () => OnQuickClickWithCallback(() => GameEventHandler.RaiseEvent(GameEventType.OnResumeButtonClick));
        _buttons[1].OnSelect += () => OnQuickClickWithCallback(() => { GameEventHandler.RaiseEvent(GameEventType.ResetGameSpeed); MyScene.Manager.LoadTargetScene(0); });
        _buttons[2].OnSelect += () => OnImpactClickWithCallback(()=> { GameEventHandler.RaiseEvent(GameEventType.ResetGameSpeed); MyScene.Manager.LoadTargetScene(1); });
        _buttons[3].OnSelect += () => OnQuickClickWithCallback(() => Application.Quit());
    }

    private void OnQuickClickWithCallback(System.Action callback)
    {
        SoundOverrider.QuickClick();
        callback?.Invoke();
    }

    private void OnImpactClickWithCallback(System.Action callback)
    {
        SoundOverrider.ImpactClick();
        callback?.Invoke();
    }
}