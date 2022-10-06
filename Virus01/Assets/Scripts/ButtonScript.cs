using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonScript : MonoBehaviour
{
    // Start is called before the first frame update
    public string ABC = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    public GameObject Canvas;
    public GameObject UIPrefab;
    void Start()
    {
        
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
    public void OnAttack()
    {
        CreateKeys(10, 16);
    }
    public void OnDefense()
    {

    }
    public void OnDodge()
    {

    }
    public void CreateKeys(int minAmount, int maxAmount)
    {
        int keyAmount = UnityEngine.Random.Range(minAmount, maxAmount);
        for(int i = 0; i < keyAmount; i++)
        {
            GameObject newInstance = Instantiate(UIPrefab, Canvas.transform);
            if (newInstance.transform.GetChild(0).TryGetComponent(out TMP_Text u))
            {
                u.text = ABC[UnityEngine.Random.Range(0, 25)].ToString();
                u.transform.position = new Vector3(i, 0, 0);
            }
        }
    }

    
}
