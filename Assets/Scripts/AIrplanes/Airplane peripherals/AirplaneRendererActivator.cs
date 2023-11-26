using UnityEngine;

public class AirplaneRendererActivator : MonoBehaviour
{
    private void Awake()
    {
        ActivateRenderer();
    }

    private void ActivateRenderer()
    {
        Instantiate(AirplaneAssets.Loader.Gameobjects[0], transform).transform.SetAsFirstSibling();
    }
}