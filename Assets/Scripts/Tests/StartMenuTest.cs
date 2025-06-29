using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class StartMenuTest
{
    private GameObject menuGO;
    private StartMenu menu;

    [SetUp]
    public void SetUp()
    {
        menuGO = new GameObject("StartMenu");
        menu = menuGO.AddComponent<StartMenu>();
    }

    [UnityTest]
    public IEnumerator OnStartClick_LoadsLivingroomCutscene()
    {
        string targetScene = "LivingroomCutscene";
        bool sceneLoadTriggered = false;

        SceneManager.sceneLoaded += OnSceneLoaded;

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == targetScene)
            {
                sceneLoadTriggered = true;
            }
        }

        menu.OnStartClick();

        // Wait up to 1 second for the scene load to fire
        float timer = 0f;
        while (!sceneLoadTriggered && timer < 1f)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        SceneManager.sceneLoaded -= OnSceneLoaded;

        Assert.IsTrue(sceneLoadTriggered, $"Scene '{targetScene}' should have been loaded.");
    }

    [UnityTest]
    public IEnumerator OnTestClick_LoadsSampleScene()
    {
        string targetScene = "SampleScene";
        bool sceneLoadTriggered = false;

        GameObject stmGO = new GameObject("SceneTransitionManager");
        var stm = stmGO.AddComponent<SceneTransitionManager>();

        SceneManager.sceneLoaded += OnSceneLoaded;

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == targetScene)
            {
                sceneLoadTriggered = true;
            }
        }

        menu.OnTestClick();

        // Wait up to 1 second for the scene load to fire
        float timer = 0f;
        while (!sceneLoadTriggered && timer < 1f)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        SceneManager.sceneLoaded -= OnSceneLoaded;

        Assert.IsTrue(sceneLoadTriggered, $"Scene '{targetScene}' should have been loaded.");
    }

    [TearDown]
    public void TearDown()
    {
        Object.Destroy(menuGO);
    }
}
