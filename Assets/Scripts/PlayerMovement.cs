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
        animator = GetComponent<Animator>(); 
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
        // if (movementInput != Vector2.zero) {

        if (movementInput != Vector2.zero){
            int count = rb.Cast(
                movementInput.normalized, // Direction
                movementFilter,
                castCollisions,
                moveSpeed * Time.fixedDeltaTime + collisionOffset
            );
            foreach (var hit in castCollisions)
{
    Debug.Log("Hit: " + hit.collider.name + " on layer " + LayerMask.LayerToName(hit.collider.gameObject.layer));
}

            if (count == 0)
            {
                rb.MovePosition(rb.position + movementInput * moveSpeed * Time.fixedDeltaTime);
            }
        }
            // animator.SetBool("isMoving", true);
            // movement.x = Input.GetAxisRaw("Horizontal");
            // movement.y = Input.GetAxisRaw("Vertical");
            // animator.SetFloat("Horizontal", movement.x);
            // animator.SetFloat("Vertical", movement.y);
        // } else {
        //     animator.SetBool("isMoving", false);
        // }

    }

    void OnMove(InputValue movementValue) {
        movementInput  = movementValue.Get<Vector2>();
    }
    // public int speed = 10;
    // private Rigidbody2D characterBody;
    // private Vector2 velocity;
    // private Vector2 inputMovement;

    // void Start() 
    // {
    //     velocity = new Vector2(speed, speed);
    //     characterBody = GetComponent<Rigidbody2D>();
    // }

    // void Update()
    // {
    //     inputMovement = new Vector2 (
    //         Input.GetAxisRaw("Horizontal"),
    //         Input.GetAxisRaw("Vertical")
    //     );
    // }

    // private void FixedUpdate()
    // {
    //     Vector2 delta = inputMovement * velocity * Time.deltaTime;
    //     Vector2 newPosition = characterBody.position + delta;
    //     characterBody.MovePosition(newPosition);
    // }
}