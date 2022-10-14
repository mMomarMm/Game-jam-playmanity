using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Mainmenu : MonoBehaviour
{
    [SerializeField] AudioSource a;
    [SerializeField] Slider s;
    public void NextScene()
    {
        SceneLoader.nextScene = 2;
        SceneManager.LoadScene(1);
    }
    public void Quit() { Application.Quit(); }
    public void Volume()
    {
        a.volume = s.value;
    }
}
