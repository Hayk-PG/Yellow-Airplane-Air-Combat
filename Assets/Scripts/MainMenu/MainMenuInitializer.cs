using System.Collections;
using UnityEngine;

public class MainMenuInitializer : MonoBehaviour
{
    private WaitForSeconds _initializationDelay = new WaitForSeconds(0f);



    private void Start()
    {
        StartCoroutine(InitializeAfterDelay());
    }

    private IEnumerator InitializeAfterDelay()
    {
        yield return _initializationDelay;
        GameEventHandler.RaiseEvent(GameEventType.OnMainMenuInit);
    }
}