using UnityEngine;

public class DeactivateByTime : MonoBehaviour
{
    [SerializeField]
    private float time;


    private void OnEnable()
    {
        Invoke("Deactivate", time);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
