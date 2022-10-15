using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioScript : MonoBehaviour
{
    static AudioScript aS;
    [SerializeField] List<AudioClip> songs;
    bool shouldDestroy;
    void Awake()
    {
        if (aS == null)
            aS = this;
        if (aS != this)
            Destroy(gameObject);

        GetComponent<AudioSource>().clip = songs[SceneManager.GetActiveScene().buildIndex];
    }
    void OnDestroy()
    {
        DontDestroyOnLoad(gameObject);
        
    }
}