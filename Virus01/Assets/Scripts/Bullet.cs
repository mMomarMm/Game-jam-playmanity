using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    [SerializeField] bool shouldErase;
    [SerializeField] Vector3 direction;
    private void FixedUpdate()
    {
        transform.Translate(direction * Time.deltaTime * speed);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Shield"))
        {
            gameObject.SetActive(false);
        }
    }
}