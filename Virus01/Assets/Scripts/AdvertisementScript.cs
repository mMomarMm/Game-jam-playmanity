using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvertisementScript : MonoBehaviour
{
    [SerializeField] float speedMovementX = -1.3f;
    [SerializeField] float speedMovementY = 2;
    [SerializeField] float sinFactor = 6;

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = Vector3.zero;
        direction.x = speedMovementX - 5 * Time.deltaTime * Mathf.Pow(2, 11 - ButtonScript.enemyHealth);
        direction.y = Mathf.Sin(Time.time * sinFactor) * speedMovementY;
        transform.position += direction * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // This manages all the collisions of the enemies
        if (collision.CompareTag("Player"))
        {
            DamagePlayer();
        }
        else if (collision.CompareTag("Shield"))
        {
            if (!ButtonScript.shieldIsActive)
            {
                return;
            }
            ButtonScript.buttonScriptThis.BreakShield();
        }
        gameObject.SetActive(false);
    }
    private void DamagePlayer()
    {
        ButtonScript.buttonScriptThis.OnDamage(2);
    }
}
