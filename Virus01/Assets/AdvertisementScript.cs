using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvertisementScript : MonoBehaviour
{
    [SerializeField] float speedMovementX = -1.3f;
    [SerializeField] float speedMovementY = 2;
    [SerializeField] float sinFactor = 6;
    [SerializeField] GameObject player;
    private Animator playerAnimator;
    // Start is called before the first frame update
    void Start()
    {
        player.TryGetComponent(out Animator animator);
        playerAnimator = animator;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = Vector3.zero;
        direction.x = speedMovementX;
        direction.y = Mathf.Sin(Time.time * sinFactor) * speedMovementY;
        transform.position += direction * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // This manages all the collisions of the enemies
        if (collision.CompareTag("Player"))
        {
            //OnDamage();
            print("Player Damage");
            DamagePlayer();
        }
        else if (collision.CompareTag("Shield"))
        {
            print("Shield");
        }
        gameObject.SetActive(false);
    }
    private void DamagePlayer()
    {
        PlayerMap.Corruption += 1;
        playerAnimator.SetTrigger("Damage"); 
    }
}
