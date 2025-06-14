using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class RetryMenuTest
{
    private GameObject menuGO;
    private RetryMenu menu;

    [SetUp]
    public void SetUp()
    {
        menuGO = new GameObject("RetryMenu");
        menu = menuGO.AddComponent<RetryMenu>();
    }

    [UnityTest]
    public IEnumerator OnRetryClick_LoadsSampleScene()
    {
        string targetScene = "SampleScene";
        bool sceneLoadTriggered = false;

        SceneManager.sceneLoaded += OnSceneLoaded;

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == targetScene)
                sceneLoadTriggered = true;
        }

        menu.OnRetryClick();

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
    public IEnumerator OnExitClick_LoadsStartScene()
    {
        string targetScene = "StartScene";
        bool sceneLoadTriggered = false;

        SceneManager.sceneLoaded += OnSceneLoaded;

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == targetScene)
                sceneLoadTriggered = true;
        }

        menu.OnExitClick();

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
