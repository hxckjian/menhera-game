using System.Collections.Generic;
using UnityEngine;

public class YandereScript : MonoBehaviour
{
    public float moveSpeed = 3f;
    public Transform player;
    public float collisionOffset = 0.05f;

    public ContactFilter2D movementFilter;

    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 movement;

    private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        movementFilter.useLayerMask = true;
        movementFilter.layerMask = LayerMask.GetMask("Collision"); // <- Match your main character setup
        movementFilter.useTriggers = false;
    }

    void Update()
    {
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            movement = direction;

            // Update animator parameters
            animator.SetFloat("Horizontal", direction.x);
            animator.SetFloat("Vertical", direction.y);
            animator.SetFloat("Speed", direction.sqrMagnitude);
        }
    }

    void FixedUpdate()
    {
        if (player != null)
        {
            // Check collisions before moving
            int count = rb.Cast(
                movement.normalized,
                movementFilter,
                castCollisions,
                moveSpeed * Time.fixedDeltaTime + collisionOffset
            );

            if (count == 0)
            {
                rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
            }
            else
            {
                animator.SetFloat("Speed", 0); // Stop animation if bumping into wall
            }
        }
    }
}
