using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    GameObject child;
    void Awake()
    {
        child = transform.GetChild(0).gameObject;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale != 0)
            {
                child.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                child.SetActive(false);
                Time.timeScale = 1;
            }
        }
    }
}