using UnityEngine;
using UnityEngine.SceneManagement;

public class MyScene : MonoBehaviour
{
    public enum SceneName { Init, Menu, Game }

    public static MyScene Manager { get; private set; }
    public string InitSceneName { get; private set; } = "Init";
    public string MenuSceneName { get; private set; } = "Menu";
    public string GameSceneName { get; private set; } = "Game";
    public Scene CurrentScene { get => SceneManager.GetActiveScene(); }




    private void Awake()
    {
        CreateInstance();
    }

    private void CreateInstance()
    {
        if (Manager == null)
        {
            Manager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Load a scene based on the provided SceneName
    /// </summary>
    /// <param name="sceneName"></param>
    public void LoadScene(SceneName sceneName)
    {
        switch (sceneName)
        {
            case SceneName.Menu:

                bool isInitCurrentScene = CurrentScene.name == InitSceneName;

                if (isInitCurrentScene)
                {
                    LoadMenuScene();
                }
                else
                {
                    LoadInitScene();
                }

                break;

            case SceneName.Game:

                LoadGameScene();

                break;
        }
    }

    private void LoadInitScene()
    {
        SceneManager.LoadScene(InitSceneName);
    }

    private void LoadMenuScene()
    {
        SceneManager.LoadScene(MenuSceneName);
    }

    private void LoadGameScene()
    {
        SceneManager.LoadScene(GameSceneName);
    }
}
