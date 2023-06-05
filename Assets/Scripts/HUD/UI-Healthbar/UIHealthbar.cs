using UnityEngine;
using UnityEngine.UI;

public class UIHealthbar : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Image _imgFillLayer1;
    [SerializeField] private Image _imgFillLayer2;

    [Header("Animator Component")]
    [SerializeField] private Animator _animator;

    private const string _animationClipName = "LastHealthFillAnim";



    private void OnEnable()
    {
        Reference.Manager.PlayerEventSystem.OnPlayerEventTrigger += OnPlayerEventTrigger;
    }

    private void OnPlayerEventTrigger(PlayerEventType playerEventType, object[] data)
    {
        if(playerEventType != PlayerEventType.UpdateHealthbar)
        {
            return;
        }

        int damage = (int)data[0];
        UpdateHealthbar(damage);
    }

    /// <summary>
    /// Updates the health bar based on the amount of damage.
    /// </summary>
    /// <param name="damage">The amount of damage to update the health bar with.</param>
    private void UpdateHealthbar(int value)
    {
        float fillAmount = (float)value / 100;       
        _imgFillLayer1.fillAmount -= fillAmount;
        _animator.Play(_animationClipName, 0, 0);
    }

    /// <summary>
    /// Matches the fill amount of the second health bar layer to the first layer.
    /// </summary>
    public void MatchHealthBarValues()
    {
        _imgFillLayer2.fillAmount = _imgFillLayer1.fillAmount;
    }
}