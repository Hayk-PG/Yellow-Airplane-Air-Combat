using UnityEngine;


public class ParentChangeDestroyer : MonoBehaviour
{
    private void Awake()
    {
        if (transform.parent == null)
            Destroy(gameObject);
    }

    private void OnTransformParentChanged() => Destroy(gameObject);
}
