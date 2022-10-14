using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelSelect : MonoBehaviour
{
    [SerializeField] int nextLevel;

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.Return))
            {
                SceneLoader.nextScene = nextLevel;
                SceneManager.LoadScene(1);
            }
        }
    }
}