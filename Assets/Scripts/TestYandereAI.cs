using UnityEngine;
using UnityEngine.AI;

public class TestYandereAI : MonoBehaviour
{
    [SerializeField] private Transform player;

    private NavMeshAgent agent;
    private Animator animator;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();

        agent.updateRotation = false;
        agent.updateUpAxis = false;

        if (player == null)
        {
            GameObject foundPlayer = GameObject.FindGameObjectWithTag("Player");
            if (foundPlayer != null)
            {
                player = foundPlayer.transform;
            }
        }
    }

    private void Update()
    {
        if (player != null)
        {
            // Move toward player
            agent.SetDestination(player.position);

            // Get current velocity from NavMeshAgent
            Vector2 velocity = agent.velocity;

            // Only update animator if moving
            if (velocity.sqrMagnitude > 0.01f)
            {
                animator.SetFloat("Horizontal", velocity.x);
                animator.SetFloat("Vertical", velocity.y);
                animator.SetFloat("Speed", velocity.sqrMagnitude);
            }
            else
            {
                animator.SetFloat("Speed", 0f);
            }
        }
    }
}
