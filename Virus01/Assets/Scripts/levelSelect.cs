using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelSelect : MonoBehaviour
{
    [SerializeField] int nextLevel;

    void OnTriggerStay2D(Collider2D other)
    {
        print("trigger");
        if (other.CompareTag("Player"))
        {
            print("player");
            if (Input.GetKey(KeyCode.Return))
            {
                print("enter");
                SceneLoader.nextScene = nextLevel;
                SceneManager.LoadScene(1);
            }
        }
    }
}