using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 8f;

    public float collisionOffset = 0.05f;

    public ContactFilter2D movementFilter;

    Vector2 movementInput;

    Rigidbody2D rb;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (movementInput != Vector2.zero) {

            int count = rb.Cast(
                movementInput,
                movementFilter,
                castCollisions,
                moveSpeed * Time.fixedDeltaTime + collisionOffset
            );

            if(count == 0) {
                rb.MovePosition(rb.position + movementInput * moveSpeed * Time.fixedDeltaTime);
            }
        }
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