using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    [Tooltip("if tru it means to change to blue and false change to red")]
    [SerializeField] bool changeColorToBlue;
    [SerializeField] Vector3 direction;
    [SerializeField] float whenEnd;
    float hits;
    private void Start()
    {
        if (whenEnd != 0)
        {
            StartCoroutine(EndWaveAtTime());
        }
        undertaleFightManager.thisScript.ChangeMode(changeColorToBlue);
    }
    private void FixedUpdate()
    {
        transform.Translate(direction * Time.deltaTime * speed);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            hits += 1;
        }
        if (other.CompareTag("Shield"))
        {
            gameObject.SetActive(false);
        }
    }
    IEnumerator EndWaveAtTime()
    {
        yield return new WaitForSeconds(whenEnd);
        EndWave();
    }
    public void EndWave()
    {
        hits -= 1;
        undertaleFightManager.thisScript.indexWave += 1;
        undertaleFightManager.thisScript.SetSharkHealth(7 - (hits / 2));
        undertaleFightManager.thisScript.NextWave();
    }
}