using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUndertale : MonoBehaviour
{
    [SerializeField] float speedMov, offset, rayDist, jumpForce, jumpTimeCounter;
    [SerializeField] Transform Bar, endPanel;
    [SerializeField] TMP_Text text;
    [SerializeField] LayerMask groundLayer;
    public bool canRotate;
    Rigidbody2D rb;
    float xInput, yInput, jumpTime;
    bool jumping, yInputDown;
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
            dir.x = xInput * speedMov;
            dir.y = rb.velocity.y;

            if (Physics2D.Raycast(transform.position, Vector2.down, rayDist, groundLayer))
            {
                if (yInput > 0)
                {
                    dir.y = jumpForce;
                    yInputDown = true;
                    jumping = true;
                    jumpTime = jumpTimeCounter;
                }
            }
            if (yInput > 0 && jumping)
            {
                if (!yInputDown)
                {
                    yInputDown = true;
                    jumpTime = jumpTimeCounter;
                }
                if (jumpTime > 0)
                {
                    dir.y = jumpForce;
                    jumpTime -= Time.deltaTime;
                }
                else jumping = false;
            }
            else
            {
                dir.y = rb.velocity.y;
                yInputDown = false;
                jumping = false;
            }
            rb.velocity = dir;
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
        if (PlayerMap.Corruption > 100)
        {
            StartCoroutine(Crash());
        }
    }
    IEnumerator Crash()
    {
        endPanel.gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);
        Application.Quit();
    }
}