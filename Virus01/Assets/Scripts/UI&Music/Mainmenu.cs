using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Mainmenu : MonoBehaviour
{
    static float volume = 0.05f;
    AudioSource a;
    void Awake()
    {
        a = FindObjectOfType<AudioSource>();

        a.volume = volume;
    }
    public void NextScene()
    {
        SceneLoader.nextScene = 2;
        SceneManager.LoadScene(1);
    }
    public void Quit() { Application.Quit(); }
    public void Volume(Slider s)
    {
        volume = s.value * s.value;
        a.volume = volume;
    }
}