using System.Collections;
using System.Reflection;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class InteractionSceneTransitionTest
{
    private const string testScene = "SampleScene_InteractionTest";

    private const string blankScene = "BlankScene";

    private IEnumerator ReloadTestScene()
    {
        yield return SceneManager.LoadSceneAsync(blankScene);
        yield return null;

        yield return SceneManager.LoadSceneAsync(testScene);
        yield return null;
    }

    private IEnumerator RunSingleInteractionTest(string gameObjectName, string expectedTargetScene)
    {
        yield return SceneManager.LoadSceneAsync(testScene);
        yield return null;

        GameObject obj = GameObject.Find(gameObjectName);
        Assert.IsNotNull(obj, $"GameObject '{gameObjectName}' not found in scene '{testScene}'");

        var component = obj.GetComponent<MonoBehaviour>();
        Assert.IsNotNull(component, $"No MonoBehaviour found on '{gameObjectName}'");

        MethodInfo method = component.GetType().GetMethod("OnSceneClick", BindingFlags.NonPublic | BindingFlags.Instance);
        Assert.IsNotNull(method, $"OnSceneClick() not found on '{gameObjectName}'");

        bool sceneLoaded = false;
        SceneManager.sceneLoaded += OnSceneLoaded;

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == expectedTargetScene)
                sceneLoaded = true;
        }

        method.Invoke(component, null);

        float timer = 0f;
        while (!sceneLoaded && timer < 1f)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        SceneManager.sceneLoaded -= OnSceneLoaded;
        Assert.IsTrue(sceneLoaded, $"Expected scene '{expectedTargetScene}' was not loaded.");
    }

    [UnityTest]
    public IEnumerator TableInteraction_TransitionsTo_TableScene()
    {
        yield return ReloadTestScene();
        yield return RunSingleInteractionTest("TestTableInteraction", "TableScene");
    }

    [UnityTest]
    public IEnumerator LockerInteraction_TransitionsTo_LockerScene()
    {
        yield return ReloadTestScene();
        yield return RunSingleInteractionTest("TestLockerInteraction", "LockerScene");
    }

    [UnityTest]
    public IEnumerator SwitchInteraction_TransitionsTo_SwitchScene()
    {
        yield return ReloadTestScene();
        yield return RunSingleInteractionTest("TestSwitchInteraction", "SwitchScene");
    }
}
