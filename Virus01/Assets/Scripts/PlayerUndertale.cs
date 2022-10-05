using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUndertale : MonoBehaviour
{
    [SerializeField] float speedMov;
    Rigidbody2D rb;
    float xInput, yInput;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
    }
    void FixedUpdate()
    {
        rb.velocity = new Vector2(xInput, yInput).normalized;
    }
}