using UnityEngine;

public class DestroyByTime : MonoBehaviour
{
    [Header("Destroy Time")]
    [SerializeField] private float _time;




    private void Awake()
    {
        Invoke("DestroyAfterDelay", _time);
    }

    private void DestroyAfterDelay()
    {
        Destroy(gameObject);
    }
}
