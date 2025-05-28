using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 8f;

    public float collisionOffset = 0.05f;

    public ContactFilter2D movementFilter;

    Vector2 movementInput;
    Vector2 movement;
    Animator animator;

    Rigidbody2D rb;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // animator = GetComponent<Animator>(); 
        animator = GetComponentInChildren<Animator>();
        movementFilter.useLayerMask = true;
        movementFilter.layerMask = LayerMask.GetMask("Collision"); // ONLY check against Ground
        movementFilter.useTriggers = false;
    }
    void Update()
    {
        animator.SetFloat("Horizontal", movementInput.x);
        animator.SetFloat("Vertical", movementInput.y);
        animator.SetFloat("Speed", movementInput.sqrMagnitude);
    }

    private void FixedUpdate()
    {
        if (movementInput != Vector2.zero)
        {
            bool success = Movement(movementInput);

            if (!success)
            {
                success = Movement(new Vector2(movementInput.x, 0));

                if (!success)
                {
                    success = Movement(new Vector2(0, movementInput.y));
                }
            }
        }
    }

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


    void OnMove(InputValue movementValue) {
        movementInput  = movementValue.Get<Vector2>();
    }
}