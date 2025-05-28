using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YanderePathfinding : MonoBehaviour
{
    public Node currentNode;
    public List<Node> path = new List<Node>();

    private Animator animator;

    public float speed = 3f;
    public PlayerMovement player;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
 
        // Only try to engage when path is empty
        if (path.Count == 0)
        {
            Node targetNode = AStarManager.instance.FindNearestNode(player.transform.position);
            path = AStarManager.instance.GeneratePath(currentNode, targetNode);
        }

        // Follow the path
        CreatePath();
    }

    public void CreatePath()
    {
        // if (path.Count > 0)
        // {
        //     int x = 0;
        //     transform.position = Vector3.MoveTowards(transform.position, new Vector3(path[x].transform.position.x, path[x].transform.position.y, -2), (speed) * Time.deltaTime);

        //     if (Vector2.Distance(transform.position, path[x].transform.position) < 0.1f)
        //     {
        //         currentNode = path[x];
        //         path.RemoveAt(x);
        //     }
        // }
        if (path.Count > 0)
        {
            Node nextNode = path[0];

            Vector3 targetPosition = new Vector3(nextNode.transform.position.x, nextNode.transform.position.y, -2);
            Vector3 moveDirection = (targetPosition - transform.position).normalized;

            if (animator != null)
            {
                animator.SetFloat("Horizontal", moveDirection.x);
                animator.SetFloat("Vertical", moveDirection.y);
                animator.SetFloat("Speed", moveDirection.sqrMagnitude); // Will be close to 1 when moving
            }

            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, nextNode.transform.position) < 0.1f)
            {
                currentNode = nextNode;
                path.RemoveAt(0);
            }
        }
        else
        {
            if (animator != null)
            {
                animator.SetFloat("Speed", 0f);
            }
        }
    }
}
