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
        UpdateHealthbar(playerEventType, data);
    }

    /// <summary>
    /// Updates the health bar based on the specified player event type and data.
    /// </summary>
    /// <param name="playerEventType">The type of player event.</param>
    /// <param name="data">The data associated with the player event.</param>
    private void UpdateHealthbar(PlayerEventType playerEventType, object[] data)
    {
        if (playerEventType != PlayerEventType.UpdateHealthbarOnDamage && playerEventType != PlayerEventType.UpdateHealthbarOnRepair)
        {
            return;
        }

        UpdateFillAmountAndPlayAnimation(isDamage: playerEventType == PlayerEventType.UpdateHealthbarOnDamage, amount: (int)data[0]);
    }

    /// <summary>
    /// Updates the fill amount of the health bar layers and plays the animation.
    /// </summary>
    /// <param name="isDamage">Indicates whether the update is for damage.</param>
    /// <param name="amount">The amount of damage or repair.</param>
    private void UpdateFillAmountAndPlayAnimation(bool isDamage, int amount)
    {
        float fillAmount = (float)amount / 100;
        _imgFillLayer1.fillAmount = isDamage ? _imgFillLayer1.fillAmount - fillAmount : _imgFillLayer1.fillAmount + fillAmount;
        _animator.Play(_animationClipName, 0, 0);
    }

    /// <summary>
    /// Matches the fill amount of the second health bar layer to the first layer.
    /// This method is triggered through an animation event.
    /// </summary>
    public void MatchHealthBarValues()
    {
        _imgFillLayer2.fillAmount = _imgFillLayer1.fillAmount;
    }
}