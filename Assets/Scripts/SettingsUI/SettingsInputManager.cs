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
        _buttons[1].OnSelect += () => OnQuickClickWithCallback(() => MyScene.Manager.LoadTargetScene(0));
        _buttons[2].OnSelect += () => OnImpactClickWithCallback(()=> MyScene.Manager.LoadTargetScene(1));
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