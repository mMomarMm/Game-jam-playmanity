using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    private Vector3 velocity = new Vector3(7.5f, 0, 0);
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameObject.activeSelf) return;
        transform.position += Time.deltaTime * velocity;
        //for(int i = 0; i < transform.childCount; i++)
        //{
        //    transform.GetChild(i).position += Time.deltaTime * velocity;
        //}
    }
}
