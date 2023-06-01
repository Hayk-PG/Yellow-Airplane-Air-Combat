using System;
using UnityEngine;

public class SpriteRendererAnimationManager : MonoBehaviour
{
    // Event raised when the frame of the sprite animation changes
    public event Action OnAnimationFrameChange;




    /// <summary>
    /// Raises the animation frame change event, indicating that the frame of the sprite animation has changed.
    /// </summary>
    public void RaiseAnimationFrameChangeEvent()
    {
        OnAnimationFrameChange?.Invoke();
    }
}