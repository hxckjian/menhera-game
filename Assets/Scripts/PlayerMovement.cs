using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float collisionOffset = 0.05f;
    [SerializeField] private ContactFilter2D movementFilter;

    private Vector2 movementInput;
    private Animator animator;
    private Rigidbody2D rb;
    private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    private Vector2 lastMovementDirection;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();

        // Ensure only "Collision" layer is considered and triggers are ignored
        movementFilter.useLayerMask = true;
        movementFilter.layerMask = LayerMask.GetMask("Collision"); 
        movementFilter.useTriggers = false;
    }

    //Returns last facing vector
    public Vector2 FacingVector => lastMovementDirection;

    public Direction FacingDirection
    {
        get
        {
            if (lastMovementDirection == Vector2.up)
                return Direction.Up;
            if (lastMovementDirection == Vector2.down)
                return Direction.Down;
            if (lastMovementDirection == Vector2.left)
                return Direction.Left;
            if (lastMovementDirection == Vector2.right)
                return Direction.Right;

            return Direction.None;
        }
    }

    // Adjusts Animator paremeters to allocate correct animation when moving in certain direction
    private void Update()
    {
        animator.SetFloat("Speed", movementInput.sqrMagnitude);

        if (movementInput.sqrMagnitude > 0.01f)
        {
            animator.SetFloat("Horizontal", movementInput.x);
            animator.SetFloat("Vertical", movementInput.y);
            lastMovementDirection = movementInput;
        }
        else
        {
            // Apply last movement direction when idle
            animator.SetFloat("Horizontal", lastMovementDirection.x);
            animator.SetFloat("Vertical", lastMovementDirection.y);
        }
    }

    private void FixedUpdate()
    {
        if (movementInput != Vector2.zero)
        {
            bool success = Movement(movementInput)
             || Movement(new Vector2(movementInput.x, 0))
             || Movement(new Vector2(0, movementInput.y));
        }
    }

    // Attempts to move player in specified direction and Cast is used to check for collisions
    private bool Movement(Vector2 direction)
    {
        if (direction == Vector2.zero)
            return false;

        int count = rb.Cast(
            direction.normalized,
            movementFilter,
            castCollisions,
            moveSpeed * Time.fixedDeltaTime + collisionOffset
        );

        if (count == 0)
        {
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
            return true;
        }

        return false;
    }

    // Receives movement input from the Input System
    private void OnMove(InputValue movementValue) {
        movementInput  = movementValue.Get<Vector2>();
    }
}