using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class SpriteSorterTest
{
    private GameObject go;
    private SpriteRenderer sr;
    private SpriteSorter sorter;

    [SetUp]
    public void SetUp()
    {
        go = new GameObject("TestSprite");
        sr = go.AddComponent<SpriteRenderer>();
        sorter = go.AddComponent<SpriteSorter>();

        typeof(SpriteSorter).GetField("offset", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(sorter, 0);
        typeof(SpriteSorter).GetField("baseOrder", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(sorter, 1000);
    }

    [UnityTest]
    public IEnumerator SortingOrder_UpdatesBasedOnYPosition()
    {
        go.transform.position = new Vector3(0f, 1f, 0f);

        yield return new WaitForEndOfFrame(); 

        int expectedOrder = 1000 - Mathf.RoundToInt(1f * 100f);
        Assert.AreEqual(expectedOrder, sr.sortingOrder);
    }

    [UnityTest]
    public IEnumerator SortingOrder_WithNegativeY()
    {
        go.transform.position = new Vector3(0f, -2.5f, 0f);

        yield return new WaitForEndOfFrame();

        int expectedOrder = 1000 - Mathf.RoundToInt(-2.5f * 100f);
        Assert.AreEqual(expectedOrder, sr.sortingOrder);
    }

    [TearDown]
    public void TearDown()
    {
        Object.Destroy(go);
    }
}
