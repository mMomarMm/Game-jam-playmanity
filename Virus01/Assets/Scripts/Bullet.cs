using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    [SerializeField] bool shouldErase;

    void Update()
    {

    }
    private void FixedUpdate()
    {
        transform.Translate(transform.up * Time.deltaTime * speed);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Shield"))
        {
            gameObject.SetActive(false);
        }
    }
}