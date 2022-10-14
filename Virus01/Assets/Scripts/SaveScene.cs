using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveScene : MonoBehaviour
{
    public static int musicVol;
    AudioSource b;

    // Start is called before the first frame update
    void Awake()
    {
        b = GameObject.FindWithTag("Player").GetComponent<AudioSource>();
        b.volume = musicVol;
    }
    public static void ChangeVolume() { }
    public static void SaveData()
    {

    }
}
