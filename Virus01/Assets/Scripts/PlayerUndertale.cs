using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUndertale : MonoBehaviour
{
    [SerializeField] float speedMov, offset;
    Rigidbody2D rb;
    float xInput, yInput;
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
    }
    void FixedUpdate()
    {
        var dir = new Vector2(xInput, yInput).normalized;
        rb.position = rb.position + dir * speedMov * Time.deltaTime;

        if (xInput != 0 || yInput != 0)
            rb.rotation = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + offset;

        else
            rb.rotation = 0;
    }
}