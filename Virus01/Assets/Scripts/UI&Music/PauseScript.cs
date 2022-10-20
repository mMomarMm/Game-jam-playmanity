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
                Cursor.visible = true;
                child.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                Cursor.visible = false;
                child.SetActive(false);
                Time.timeScale = 1;
            }
        }
    }
}