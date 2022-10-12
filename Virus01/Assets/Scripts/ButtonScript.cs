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
    private Dictionary<int, List<GameObject>> commandDict = new Dictionary<int, List<GameObject>>();

    // Sprites
    private Sprite attackSprite, defenseSprite, dodgeSprite;
    public Sprite attackOnClickSprite, defenseOnClickSprite, dodgeOnClickSprite;

    private Dictionary<int, Dictionary<string, object>> dataDict;

    private int lastClicked = 10;

    // Start is called before the first frame update
    void Start() {
        attackButton.TryGetComponent(out Image attackImg);
        defenseButton.TryGetComponent(out Image defenseImg);
        dodgeButton.TryGetComponent(out Image dodgeImg);

        attackSprite = attackImg.sprite;
        defenseSprite = defenseImg.sprite;
        dodgeSprite = dodgeImg.sprite;

        for(sbyte i = 0; i < 3; i++) commandDict.Add(i, new List<GameObject>());

        dataDict = new Dictionary<int, Dictionary<string, object>>{
            {
                0, new Dictionary<string, object>
                {
                    {"minAmount", 10 },
                    {"maxAmount", 16 },
                    {"objectSprite", attackSprite },
                    {"onclickSprite", attackOnClickSprite },
                    {"objectButton", attackButton}
                }
            },
            {
                1, new Dictionary<string, object>
                {
                    {"minAmount", 6 },
                    {"maxAmount", 8 },
                    {"objectSprite", defenseSprite },
                    {"onclickSprite", defenseOnClickSprite },
                    {"objectButton", defenseButton}
                }
            },
            {
                2, new Dictionary<string, object>
                {
                    {"minAmount", 2 },
                    {"maxAmount", 2 },
                    {"objectSprite", dodgeSprite },
                    {"onclickSprite", dodgeOnClickSprite },
                    {"objectButton", dodgeButton}
                }
            },
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SelectAction(int objectId)
    {
        if(lastClicked == objectId)
        {
            return;
        }
        lastClicked = objectId;
        
        Dictionary<string, object> data = dataDict[objectId];
        int minAmount = (int)data["minAmount"];
        int maxAmount = (int)data["maxAmount"];
        Sprite objectSprite = (Sprite)data["objectSprite"];
        Sprite onclickSprite = (Sprite)data["onclickSprite"];
        GameObject buttonObject = (GameObject)data["objectButton"];

        ChangeImage(buttonObject, onclickSprite);
        StartCoroutine(ResetImage(buttonObject, objectSprite));

        DestroyObjects();

        List<GameObject> attackList = commandDict[objectId];

        int length = attackList.Count;
        print(length);
        if (length > 0)
        {
            foreach (GameObject attack in attackList)
            {
                attack.SetActive(true);
            }
            return;
        }
        // else
        CreateKeys(objectId, minAmount, maxAmount);

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
    private void CreateKeys(int objectId, int minAmount, int maxAmount)
    {
        DestroyObjects();
        int keyAmount = UnityEngine.Random.Range(minAmount, maxAmount);
        for(int i = 0; i < keyAmount; i++)
        {
            GameObject newInstance = Instantiate(UIPrefab, container.transform);
            newInstance.transform.position = new Vector2(MoveKey(keyAmount, i), 0);

            if (newInstance.transform.GetChild(0).TryGetComponent(out TMP_Text u))
            {
                string keyStr = ABC[UnityEngine.Random.Range(0, 25)].ToString();
                u.text = keyStr;
            }
            commandDict[objectId].Add(newInstance);
        }
    }
    private float MoveKey(int n, int i)
    {        
        float containerWidth = referenceFinalObject.transform.position.x;
        float xPosition = (containerWidth / 16) + (spaceBetween * (i - (n / 2)));
        return xPosition;
    }

    public void DestroyObjects()
    {
        for (int i = 0; i < container.transform.childCount; i++)
        {
            container.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
