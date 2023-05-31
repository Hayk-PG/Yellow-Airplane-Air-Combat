using UnityEngine;

public class DestroyByTime : MonoBehaviour
{
    [SerializeField] float time;


    void Awake()
    {
        Invoke("Dest", time);
    }

    void Dest()
    {
        Destroy(gameObject);
    }
}
