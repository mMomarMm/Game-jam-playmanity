using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class SceneLoader : MonoBehaviour
{
    public static int nextScene;
    public TMP_Text porcentage, tip;
    AsyncOperation s;
    float time;
    string[] tips = new string[]{"Dying is bad", "If you contaminate i'll kill you", "Plastic bags are made out of plastic",
    "Apples taste like apples", "Game jams are streesfull", "Do you like the font?",
    "Subscribe to my youtube... pls", "A computer mouse isn't a real mouse",
    "Inalambric computer mouse should be call hamster", "Drinking water will hydrate you",
    "Ignore the grammatical mistakes",
    "Don't dodge the friendly pellets", "Remember to keep your corruption low", "Play paper bag boy on itch.io", "Hotel? trivago",
    "Pepsi is better than Dr. Pepper", "F for technoblade", "Dr. Pepper is the intellectual drink",
    "You won an ePhone 17s xcv max pro, CLICK_HERE to claim"};
    void Start()
    {
        time = 0;
        s = SceneManager.LoadSceneAsync(nextScene);
        s.allowSceneActivation = false;
        tip.text = tips[Random.Range(0, tips.Length)];
    }
    private void Update()
    {
        if (time >= 3.2f)
        {
            s.allowSceneActivation = true;
        }
        else
        {
            time += Time.deltaTime;
        }
    }
}
