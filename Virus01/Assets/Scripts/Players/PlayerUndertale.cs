using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUndertale : MonoBehaviour
{
    [SerializeField] float speedMov, offset, rayDist, jumpForce;
    [SerializeField] Transform Bar;
    [SerializeField] TMP_Text text;
    [SerializeField] LayerMask groundLayer;
    public bool canRotate;
    Rigidbody2D rb;
    float xInput, yInput;
    void Start()
    {
        text.text = PlayerMap.Corruption.ToString();
        var e = Bar.localScale;
        e.x = PlayerMap.Corruption;
        Bar.localScale = e;

        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        if (canRotate)
        {
            var dir = new Vector2(xInput, yInput).normalized;
            rb.position += dir * speedMov * Time.deltaTime;

            if (xInput != 0 || yInput != 0)
                rb.rotation = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + offset;
            else
                rb.rotation = 0;
        }
        else
        {
            bool canJump = Physics2D.Raycast(transform.position, Vector2.down, rayDist, groundLayer);
            Vector2 dir;
            dir.x = xInput * speedMov * Time.deltaTime;
            if (yInput > 0 && Physics2D.Raycast(transform.position, Vector2.down, rayDist, groundLayer))
                dir.y = jumpForce;
            else dir.y = rb.velocity.y;
            rb.position += dir * speedMov * Time.deltaTime;
        }

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            float damage = 0.5f;
            PlayerMap.Corruption += damage;
            HealthBar(damage);
        }
    }
    void HealthBar(float damage)
    {
        text.text = PlayerMap.Corruption.ToString() + "%";
        var e = Vector3.zero;
        e.x += damage;
        Bar.localScale += e;
    }
}