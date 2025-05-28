using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Node cameFrom;
    public List<Node> connections;

    public float gScore;
    public float hScore;

    public float FScore()
    {
        return gScore + hScore;
    }

    private void OnDrawGizmos()
    {
        if(connections.Count > 0)
        {
            Gizmos.color = Color.blue;
            for(int i = 0; i < connections.Count; i++)
            {
                Gizmos.DrawLine(transform.position, connections[i].transform.position);
            }
        }
    }

    [ContextMenu("Auto Connect Neighbors (4-Directions)")]
    public void AutoConnect()
    {
    connections = new List<Node>();
    Node[] allNodes = FindObjectsByType<Node>(FindObjectsSortMode.None);
    float snap = 0.01f;
    float spacing = 0.5f;

    foreach (Node other in allNodes)
    {
        if (other == this) continue;

        Vector2 diff = other.transform.position - transform.position;

        if (Mathf.Abs(diff.x) < snap && Mathf.Abs(diff.y - spacing) < snap) // up
            connections.Add(other);
        else if (Mathf.Abs(diff.x) < snap && Mathf.Abs(diff.y + spacing) < snap) // down
            connections.Add(other);
        else if (Mathf.Abs(diff.x - spacing) < snap && Mathf.Abs(diff.y) < snap) // right
            connections.Add(other);
        else if (Mathf.Abs(diff.x + spacing) < snap && Mathf.Abs(diff.y) < snap) // left
            connections.Add(other);
    }
    }

    public void AutoConnect4()
{
    connections = new List<Node>();
    Node[] allNodes = FindObjectsByType<Node>(FindObjectsSortMode.None);
    float snap = 0.01f;
    float spacing = 0.5f; 

    foreach (Node other in allNodes)
    {
        if (other == this) continue;

        Vector2 diff = other.transform.position - transform.position;

        if (Mathf.Abs(diff.x) < snap && Mathf.Abs(diff.y - spacing) < snap) // up
            connections.Add(other);
        else if (Mathf.Abs(diff.x) < snap && Mathf.Abs(diff.y + spacing) < snap) // down
            connections.Add(other);
        else if (Mathf.Abs(diff.x - spacing) < snap && Mathf.Abs(diff.y) < snap) // right
            connections.Add(other);
        else if (Mathf.Abs(diff.x + spacing) < snap && Mathf.Abs(diff.y) < snap) // left
            connections.Add(other);
    }
}

}
