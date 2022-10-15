using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Reflection;


public class ButtonScript : MonoBehaviour
{
    private readonly string ABC = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    private readonly float spaceBetween = 1f;
    private int lastClicked = 10;
    private int codeId = 0;

    private Dictionary<int, Dictionary<string, object>> dataDict;
    private Dictionary<int, List<GameObject>> commandDict = new Dictionary<int, List<GameObject>>();
    #region GameObjects
    public GameObject container;

    public GameObject referenceFinalObject;
    
    // Key game object
    public GameObject UIPrefab;

    // Button game objects
    public GameObject attackButton, defenseButton, dodgeButton;

    public GameObject correctContainer, incorrectContainer;

    public GameObject player;

    public GameObject shield;
    #endregion
    #region Sprites
    // Sprites
    private Sprite attackSprite, defenseSprite, dodgeSprite;
    public Sprite attackOnClickSprite, defenseOnClickSprite, dodgeOnClickSprite;
    public Sprite attackKeySprite, defenseKeySprite, dodgeKeySprite;
    #endregion

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
        if (Time.timeScale == 0) return;
        if (lastClicked == 10) return;

        Transform firstChild;
        List<GameObject> currentList = commandDict[lastClicked];
        try
        {
            firstChild = currentList[0].transform.GetChild(0);
        } catch // There are no more objects
        {
            switch (lastClicked)
            {
                case 0: OnAttack();  break;
                case 1: OnDefense();  break;
                case 2: OnDodge(); break;
            }
            CreateKeys(lastClicked);
            return;
        }
        firstChild.TryGetComponent(out TMP_Text u);
        string charToRemove = u.text;

        if (
            !Input.anyKeyDown || Input.GetMouseButtonDown(0) ||
            Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Escape)
            ) return;
        if (Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), charToRemove)))
        {
            correctContainer.SetActive(true);
            incorrectContainer.SetActive(false);

            Destroy(commandDict[lastClicked][0]);
            commandDict[lastClicked].RemoveAt(0);
            
            AddCode();
            codeId += 1;
            return;
        }
        // else
        correctContainer.SetActive(false);
        incorrectContainer.SetActive(true);
    }
    public void SelectAction(int objectId)
    {
        if(lastClicked == objectId) return;

        lastClicked = objectId;


        ResetButtons();
        ChangeImage();
        DestroyObjects();

        List<GameObject> attackList = commandDict[objectId];

        if (attackList.Count > 0)
        {
            foreach (GameObject attack in attackList)
            {
                attack.SetActive(true);
            }
        }

    }
    private void ChangeImage()
    {
        Dictionary<string, object> lastClickedData = dataDict[lastClicked];

        GameObject imgObject = (GameObject)lastClickedData["objectButton"];
        Sprite newSprite = (Sprite)lastClickedData["onclickSprite"];

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
            newInstance.transform.localPosition = new Vector2(0, 0);
            newInstance.transform.position += new Vector3(MoveKey(keyAmount, i), 0);
            
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
        float xPosition = (containerWidth / 20) + (spaceBetween * (i - (n / 2)));
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
    private void ResetButtons()
    {
        List<GameObject> buttons = new List<GameObject> { attackButton, defenseButton, dodgeButton }; 
        for(int i = 0; i < buttons.Count; i++)
        {
            GameObject gameObj = buttons[i];
            gameObj.TryGetComponent(out Image image);
            image.sprite = (Sprite)dataDict[i]["objectSprite"];

        }
    }
    private void AddCode()
    {
        ActivateChild(correctContainer);
        ActivateChild(incorrectContainer);
    }

    // Activates the codeId child of the object passed
    private void ActivateChild (GameObject parentObject)
    {
        if(codeId == 9)
        {
            DeactivateRange(0, 9);
        } else if (codeId == 13)
        {
            DeactivateRange(9, 13);
            codeId = 0;
        }
        Transform childObject = parentObject.transform.GetChild(codeId);
        childObject.gameObject.SetActive(true);
    }
    private void DeactivateRange(int n1, int n2)
    {
        for (int i = n1; i < n2; i++)
        {
            correctContainer.transform.GetChild(i).gameObject.SetActive(false);
            incorrectContainer.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
    private void OnAttack()
    {
        player.TryGetComponent(out Animator animator);
        animator.SetTrigger("Attack");
    }
    private void OnDefense()
    {
        shield.TryGetComponent(out Animator animator);
        animator.SetTrigger("Defense");
    }
    private void OnDodge()
    {
        player.TryGetComponent(out Animator animator);
        animator.SetTrigger("Dodge");
    }
}
