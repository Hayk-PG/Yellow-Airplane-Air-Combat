using UnityEngine;

public class Button_Sound : MonoBehaviour
{
    [SerializeField] protected int _listIndex;
    [SerializeField] protected int _clipIndex;


    public virtual void OnButtonSound()
    {
        UISoundController.PlaySound(_listIndex, _clipIndex);
    }
}
