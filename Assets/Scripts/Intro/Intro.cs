using UnityEngine;

public class Intro : MonoBehaviour
{
    // Animation Event
    private void FinalizeIntro()
    {
        MyScene.Manager.LoadTargetScene(1);
    }
}