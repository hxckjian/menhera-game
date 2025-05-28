using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class NodeGenerator : MonoBehaviour
{
    public GameObject nodePrefab;
    public int width = 10;
    public int height = 10;
    public float spacing = 0.5f;
    public Vector2 startPosition = Vector2.zero;

#if UNITY_EDITOR
    [ContextMenu("Generate Grid And Connect")]
    void GenerateGridAndConnect()
    {
        GenerateGrid();

        // Connect all nodes after generating
        Node[] allNodes = GetComponentsInChildren<Node>();
        foreach (Node node in allNodes)
        {
            node.AutoConnect4(); // custom method we'll add in a sec!
        }

        Debug.Log("Grid generated and nodes connected!");
    }

    void GenerateGrid()
    {
        // Clear previous children
        while (transform.childCount > 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Vector2 spawnPos = startPosition + new Vector2(x * spacing, y * spacing);
                GameObject newNode = (GameObject)PrefabUtility.InstantiatePrefab(nodePrefab);
                newNode.transform.position = new Vector3(spawnPos.x, spawnPos.y, 0f);
                newNode.transform.SetParent(this.transform);
            }
        }
    }
#endif
}
