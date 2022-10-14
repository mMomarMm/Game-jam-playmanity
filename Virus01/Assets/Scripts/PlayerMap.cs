using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMap : MonoBehaviour
{
    public static float Corruption = 0;
    public float speed, jumpForce, raycastDist;
    [SerializeField] LayerMask groundLayer;
    Rigidbody2D rb;
    float xInput, yInput;
    bool Jump;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");

        if (Physics2D.Raycast(transform.position, Vector2.down, raycastDist, groundLayer))
            if (yInput > 0 || Input.GetButton("Jump")) Jump = true;

    }
    void FixedUpdate()
    {
        Vector2 movement;
        movement.x = xInput * speed;
        if (Jump)
        {
            movement.y = jumpForce;
            Jump = false;
        }
        else movement.y = rb.velocity.y;
        rb.velocity = movement;
    }
}