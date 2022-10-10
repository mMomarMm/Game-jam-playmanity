using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonScript : MonoBehaviour
{
    private string ABC = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    float spaceBetween = 1.2f;

    public GameObject container;

    public GameObject referenceObject;
    public GameObject referenceFinalObject;
    
    // Key game object
    public GameObject UIPrefab;

    // Button game objects
    public GameObject attackButton, defenseButton, dodgeButton;

    // Spites
    private Sprite attackSprite, defenseSprite, dodgeSprite;
    public Sprite attackOnClickSprite, defenseOnClickSprite, dodgeOnClickSprite;


    // Start is called before the first frame update
    void Start() {
        attackButton.TryGetComponent(out Image attackImg);
        defenseButton.TryGetComponent(out Image defenseImg);
        dodgeButton.TryGetComponent(out Image dodgeImg);

        attackSprite = attackImg.sprite;
        defenseSprite = defenseImg.sprite;
        dodgeSprite = dodgeImg.sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SelectAction(int objectId)
    {
        for (int i = 0; i < container.transform.childCount; i++)
        {
            Destroy(container.transform.GetChild(i).gameObject);
        }
        switch (objectId)
        {
            // Attack
            case 0: 

                OnAttack();
                break;
            // Defense
            case 1:
                OnDefense();
                break;
            // Dodge
            case 2:
                OnDodge();
                break;
        }
    }
    private IEnumerator ResetImage(GameObject imgObject, Sprite newSprite)
    {
        yield return new WaitForSeconds(1);
        ChangeImage(imgObject, newSprite);
    }
    private void ChangeImage(GameObject imgObject, Sprite newSprite)
    {
        imgObject.TryGetComponent(out Image image);
        image.sprite = newSprite;
    }
    private void OnAttack()
    {
        ChangeImage(attackButton, attackOnClickSprite);
        StartCoroutine(ResetImage(attackButton, attackSprite));
        CreateKeys(10, 16);

    }
    private void OnDefense()
    {
        ChangeImage(defenseButton, defenseOnClickSprite);
        StartCoroutine(ResetImage(defenseButton, defenseSprite));
        CreateKeys(6, 8);
    }
    private void OnDodge()
    {
        ChangeImage(dodgeButton, dodgeOnClickSprite);
        StartCoroutine(ResetImage(dodgeButton, dodgeSprite));
        CreateKeys(2, 2);
    }
    private void CreateKeys(int minAmount, int maxAmount)
    {
        int keyAmount = UnityEngine.Random.Range(minAmount, maxAmount);
        for(int i = 0; i < keyAmount; i++)
        {
            GameObject newInstance = Instantiate(UIPrefab, container.transform);
            newInstance.transform.position = new Vector2(MoveKey(keyAmount, i), 0);

            if (newInstance.transform.GetChild(0).TryGetComponent(out TMP_Text u))
            {
                u.text = ABC[UnityEngine.Random.Range(0, 25)].ToString();
            }
        }
    }
    private float MoveKey(int n, int i)
    {        
        float containerWidth = referenceFinalObject.transform.position.x;
        float xPosition = (containerWidth / 16) + (spaceBetween * (i - (n / 2)));
        return xPosition;
    }

    
}
