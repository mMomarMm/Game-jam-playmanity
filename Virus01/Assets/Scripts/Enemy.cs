using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float sinFactor = 10, speedMovementY = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = Vector3.zero;
        direction.y = Mathf.Sin(Time.time * sinFactor) * speedMovementY;
        transform.position += direction * Time.deltaTime;
    }
}
