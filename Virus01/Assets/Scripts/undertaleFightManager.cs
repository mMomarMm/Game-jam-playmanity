using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class undertaleFightManager : MonoBehaviour
{
    public static undertaleFightManager thisScript;
    public int indexWave;
    [SerializeField] List<GameObject> attacks;
    [SerializeField] PlayerUndertale player;
    [SerializeField] float gravity, SharkHealth;
    [SerializeField] Animator sharkAnim;
    [SerializeField] TMP_Text text;
    [SerializeField] GameObject bar;
    // Start is called before the first frame update
    void Awake()
    {
        thisScript = this;
        ChangeMode();
        SetSharkHealth(0);
        NextWave();
    }

    public void NextWave()
    {
        attacks[indexWave].SetActive(true);
    }
    void ChangeMode()
    {
        player.transform.rotation = Quaternion.identity;
        player.TryGetComponent(out SpriteRenderer s);
        player.TryGetComponent(out Rigidbody2D rb);
        if (player.canRotate)
        {
            rb.gravityScale = gravity;
            s.color = Color.red;
        }
        else
        {
            rb.gravityScale = 0;
            s.color = Color.white;
        }
        player.canRotate = !player.canRotate;
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