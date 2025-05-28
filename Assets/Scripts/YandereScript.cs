using UnityEngine;
using UnityEngine.AI;

public class YandereAI : MonoBehaviour
{
    [SerializeField] Transform target;

    private NavMeshAgent agent;
    private Animator animator;
    private Vector2 lastPosition;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        animator = GetComponent<Animator>();
        lastPosition = transform.position;
    }

    private void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);
        }

        // Calculate movement direction for animation
        Vector2 currentPosition = transform.position;
        Vector2 direction = (currentPosition - lastPosition).normalized;

        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);
        animator.SetFloat("Speed", agent.velocity.sqrMagnitude);

        lastPosition = currentPosition;
    }
}
