using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class undertaleFightManager : MonoBehaviour
{
    public static undertaleFightManager thisScript;
    static bool isFirstTime = true;
    public int indexWave;
    public GameObject box;
    [SerializeField] List<GameObject> attacks;
    public PlayerUndertale player;
    [SerializeField] float gravity, SharkHealth;
    [SerializeField] Animator sharkAnim;
    [SerializeField] TMP_Text text;
    [SerializeField] GameObject bar, EnterText, endPanel, Shark, wavesChild;
    float sharkHP;

    //only to use when the first waves had happened
    static List<GameObject> wavesPrefab = new List<GameObject>();
    float barMax;
    GameObject previousWave;

    void Awake()
    {
        sharkHP = SharkHealth;
        barMax = bar.transform.localScale.x;
        if (isFirstTime)
        {
            for (int i = 2; i < attacks.Count; i++)
            {
                wavesPrefab.Add(attacks[i]);
            }
        }
        player = FindObjectOfType<PlayerUndertale>();
        thisScript = this;
        SetSharkHealth(0);
        StartCoroutine(StartGame());
    }
    IEnumerator StartGame()
    {
        bool HitEnter = false;
        while (!HitEnter)
        {
            HitEnter = Input.GetKeyDown(KeyCode.Return);
            yield return null;
        }
        EnterText.SetActive(false);
        attacks[indexWave].SetActive(true);
        previousWave = attacks[indexWave];
    }
    public void NextWave()
    {
        StartCoroutine(StartWave());
    }
    IEnumerator StartWave()
    {
        yield return new WaitForSeconds(2.5f);
        if (previousWave != null)
            Destroy(previousWave);
        previousWave = Instantiate(attacks[Random.Range(0, attacks.Count)]);
        previousWave.transform.parent = wavesChild.transform;
        //  previousWave.transform.localPosition = Vector3.zero;
        previousWave.SetActive(true);
    }
    public void ChangeMode(bool shouldBeBlue)
    {
        player.transform.rotation = Quaternion.identity;
        player.TryGetComponent(out SpriteRenderer s);
        player.TryGetComponent(out Rigidbody2D rb);
        rb.velocity = Vector2.zero;
        if (shouldBeBlue)
        {
            player.canRotate = true;
            rb.gravityScale = 0;
            s.color = Color.white;
        }
        else
        {
            player.canRotate = false;
            rb.gravityScale = gravity;
            s.color = Color.red;
        }
    }
    public void SetSharkHealth(float damage)
    {
        sharkAnim.SetTrigger("Hurt");
        sharkHP -= damage;
        text.text = sharkHP.ToString();
        var e = bar.transform.localScale;
        e.x = sharkHP / SharkHealth * barMax;
        bar.transform.localScale = e;
        if (sharkHP < 0)
        {
            EndGame();
        }
    }
    void EndGame()
    {
        endPanel.SetActive(true);
        Shark.SetActive(false);
        box.SetActive(false);
        Time.timeScale = 0;
        this.enabled = false;
    }
}