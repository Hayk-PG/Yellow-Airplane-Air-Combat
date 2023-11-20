using UnityEngine.SceneManagement;

public static class MySceneManagerExtension 
{
    public static void LoadTargetScene(this MyScene myScene, int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
