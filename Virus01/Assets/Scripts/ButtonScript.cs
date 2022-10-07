using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonScript : MonoBehaviour
{
    private string ABC = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    public GameObject UIPrefab;

    public GameObject attackButton, defenseButton, dodgeButton;

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
        switch (objectId)
        {
            // Attack
            case 0: OnAttack();
                break;
            // Defense
            case 1: OnDefense();
                break;
            // Dodge
            case 2: OnDodge();
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

    }
    private void OnDodge()
    {

    }
    private void CreateKeys(int minAmount, int maxAmount)
    {
        int keyAmount = UnityEngine.Random.Range(minAmount, maxAmount);
        for(int i = 0; i < keyAmount; i++)
        {
            GameObject newInstance = Instantiate(UIPrefab, transform);
            if (newInstance.transform.GetChild(0).TryGetComponent(out TMP_Text u))
            {
                u.text = ABC[UnityEngine.Random.Range(0, 25)].ToString();
                u.transform.position = new Vector3(i, 0, 0);
            }
        }
    }

    
}
