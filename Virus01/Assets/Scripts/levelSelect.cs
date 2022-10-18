using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelSelect : MonoBehaviour
{
    [SerializeField] int nextLevel;
    [SerializeField] Animator a;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            a.SetBool("PlayerInside", true);
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Input.GetKey(KeyCode.Return))
        {
            SceneLoader.nextScene = nextLevel;
            SceneManager.LoadScene(1);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            a.SetBool("PlayerInside", false);
        }
    }
}