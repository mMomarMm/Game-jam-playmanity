using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class undertaleFightManager : MonoBehaviour
{
    public static undertaleFightManager thisScript;
    public int indexWave;
    public GameObject box;
    [SerializeField] List<GameObject> attacks;
    [SerializeField] PlayerUndertale player;
    [SerializeField] float gravity, SharkHealth;
    [SerializeField] Animator sharkAnim;
    [SerializeField] TMP_Text text;
    [SerializeField] GameObject bar, EnterText;

    void Awake()
    {
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
    }
    public void NextWave()
    {
        StartCoroutine(StartWave());
    }
    IEnumerator StartWave()
    {
        yield return new WaitForSeconds(2f);
        attacks[Mathf.Clamp(indexWave - 1, 0, attacks.Count)].SetActive(false);
        if (indexWave == attacks.Count) indexWave = Random.Range(3, attacks.Count);
        attacks[indexWave].SetActive(true);
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
        SharkHealth -= damage;
        text.text = SharkHealth.ToString();
        var e = Vector3.zero;
        e.x = damage;
        bar.transform.localScale -= e;
    }
}