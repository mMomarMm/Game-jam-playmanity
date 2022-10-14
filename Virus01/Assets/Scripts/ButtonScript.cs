using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Reflection;


public class ButtonScript : MonoBehaviour
{
    private string ABC = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    float spaceBetween = 1.2f;
    #region GameObjects
    public GameObject container;

    public GameObject referenceObject;
    public GameObject referenceFinalObject;
    
    // Key game object
    public GameObject UIPrefab;

    // Button game objects
    public GameObject attackButton, defenseButton, dodgeButton;

    #endregion

    #region Sprites
    // Sprites
    private Sprite attackSprite, defenseSprite, dodgeSprite;
    public Sprite attackOnClickSprite, defenseOnClickSprite, dodgeOnClickSprite;
    public Sprite attackKeySprite, defenseKeySprite, dodgeKeySprite;
    #endregion

    private Dictionary<int, Dictionary<string, object>> dataDict;
    private int lastClicked = 10;
    private Dictionary<int, List<GameObject>> commandDict = new Dictionary<int, List<GameObject>>();

    // Start is called before the first frame update
    void Start() {
        Cursor.visible = true;
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
        if (lastClicked == 10) return;
        
        Transform firstChild;
        List<GameObject> currentList = commandDict[lastClicked];
        try
        {
            firstChild = currentList[0].transform.GetChild(0);
        } catch // There are no more objects
        {
            CreateKeys(lastClicked);
            return;
        }
        firstChild.TryGetComponent(out TMP_Text u);
        string charToRemove = u.text;
        if(Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), charToRemove)))
        {
            Destroy(commandDict[lastClicked][0]);
            commandDict[lastClicked].RemoveAt(0);
        }
        
    }
    public void SelectAction(int objectId)
    {
        if(lastClicked == objectId)
        {
            return;
        }
        lastClicked = objectId;
        
        Dictionary<string, object> data = dataDict[objectId];
        Sprite objectSprite = (Sprite)data["objectSprite"];
        Sprite onclickSprite = (Sprite)data["onclickSprite"];
        GameObject buttonObject = (GameObject)data["objectButton"];

        ChangeImage(buttonObject, onclickSprite);
        StartCoroutine(ResetImage(buttonObject, objectSprite));

        DestroyObjects();

        List<GameObject> attackList = commandDict[objectId];

        int length = attackList.Count;
        if (length > 0)
        {
            foreach (GameObject attack in attackList)
            {
                attack.SetActive(true);
            }
            return;
        }
        // else
        // CreateKeys(objectId, minAmount, maxAmount);

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
    private void CreateKeys(int objectId)
    {
        Dictionary<string, object> data = dataDict[objectId];
        int minAmount = (int)data["minAmount"];
        int maxAmount = (int)data["maxAmount"];
        Sprite currSprite = GetSprite();

        DestroyObjects();
        int keyAmount = UnityEngine.Random.Range(minAmount, maxAmount);
        for(int i = 0; i < keyAmount; i++)
        {
            GameObject newInstance = Instantiate(UIPrefab, container.transform);
            newInstance.transform.position = new Vector2(MoveKey(keyAmount, i), 0);
            
            newInstance.transform.GetChild(0).TryGetComponent(out TMP_Text u);
            newInstance.transform.TryGetComponent(out Image img);
            
            u.text = ABC[UnityEngine.Random.Range(0, 25)].ToString();
            img.sprite = currSprite;

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
    public Sprite GetSprite()
    {
        switch (lastClicked)
        {
            case 0:
                return attackKeySprite;
            case 1:
                return defenseKeySprite;
            case 2:
                return dodgeKeySprite;
            default:
                throw new Exception();
        }
    }
}
