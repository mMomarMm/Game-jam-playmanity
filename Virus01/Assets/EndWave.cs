using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndWave : MonoBehaviour
{
    Bullet b;
    void Start()
    {
        b = GetComponentInParent<Bullet>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
b.EndWave();
        }
    }
}