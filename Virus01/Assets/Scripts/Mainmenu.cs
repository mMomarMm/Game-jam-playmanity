using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mainmenu : MonoBehaviour
{
    public void NextScene()
    {
        SceneLoader.nextScene = 2;
        SceneManager.LoadScene(1);
    }
    public void Quit() { Application.Quit(); }
    public void Volume(int vol)
    {

    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
