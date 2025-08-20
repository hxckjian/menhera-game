using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TestTools;

public class YandereChaseTest
{
    private GameObject yandere;
    private GameObject player;
    private NavMeshAgent agent;
    private TestYandereAI ai;

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        yield return UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("SampleScene_YandereAITest");

        yield return null;
        yield return new WaitForSeconds(0.1f);

        // Disable the original main character to avoid conflicts
        var originalMC  = GameObject.FindGameObjectWithTag("Player");
        if (originalMC != null)
            originalMC.SetActive(false);
        
        // Spawn a dummy player and place it at a testable position
        player = Object.Instantiate(Resources.Load<GameObject>("Prefabs/DummyMC"));
        player.transform.position = new Vector3(-2, 1, 0);

         // Spawn the dummy yandere and place it elsewhere that uses the current chasing script that is used
        yandere = Object.Instantiate(Resources.Load<GameObject>("Prefabs/DummyYandere"));
        yandere.transform.position = new Vector3(5, 0, 0);

        yield return null;

        agent = yandere.GetComponent<NavMeshAgent>();
        ai = yandere.GetComponent<TestYandereAI>();

        ai.InjectPlayer(player.transform);
        ai.BeginChase();

        yield return null;
    }

    [UnityTest]
    public IEnumerator Yandere_SetsDestinationTowardPlayer()
    {
        float timeout = 1f;
        float elapsed = 0f;

        while (Vector3.Distance(agent.destination, player.transform.position) > 0.05f && elapsed < timeout)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }

        Assert.AreEqual(player.transform.position.x, agent.destination.x, 0.05f);
        Assert.AreEqual(player.transform.position.y, agent.destination.y, 0.05f);

        Assert.AreEqual("DummyMC(Clone)", ai.GetTargetName(), "Yandere should be chasing DummyMC");
    }

    [TearDown]
    public void TearDown()
    {
        if (player != null)
    {
        player.SetActive(false);
        Object.DestroyImmediate(player);
    }

    if (yandere != null)
    {
        yandere.SetActive(false);
        Object.DestroyImmediate(yandere);
    }
    }
}
