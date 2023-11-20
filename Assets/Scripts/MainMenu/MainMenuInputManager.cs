using UnityEngine;

public class MainMenuInputManager : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Btn _playButton;
    [SerializeField] private Btn _quitButton;




    private void OnEnable()
    {
        _playButton.OnSelect += () => OnImpactClickWithCallback(()=> MyScene.Manager.LoadTargetScene(1));                  
        _quitButton.OnSelect += () => OnQuickClickWithCallback(() => Application.Quit());
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
