using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.Mathematics;

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

    public GameObject player, shield, bullets;

    public GameObject advertisementPrefab, enemy, referenceObject2;
    #endregion
    #region Sprites
    // Sprites
    private Sprite attackSprite, defenseSprite, dodgeSprite;
    public Sprite attackOnClickSprite, defenseOnClickSprite, dodgeOnClickSprite;
    public Sprite attackKeySprite, defenseKeySprite, dodgeKeySprite;
    public Sprite advertisement1, advertisement2, advertisement3;
    #endregion
    private Animator playerAnimator, shieldAnimator;
    public static bool shieldIsActive = false;
    public static ButtonScript buttonScriptThis;

    private Vector3 bulletsPosition;
    public static int enemyHealth = 10;
    private GameObject currentAdvertisement;

    // Sorry god
    public static GameObject publicPlayer;
    public static bool isInDodge = false;
    public GameObject EndPanel, winPanel, popUp;

    private bool win = false;
    // Start is called before the first frame update

    void Start()
    {
        Cursor.visible = true;
        attackButton.TryGetComponent(out Image attackImg);
        defenseButton.TryGetComponent(out Image defenseImg);
        dodgeButton.TryGetComponent(out Image dodgeImg);

        attackSprite = attackImg.sprite;
        defenseSprite = defenseImg.sprite;
        dodgeSprite = dodgeImg.sprite;

        for (sbyte i = 0; i < 3; i++) commandDict.Add(i, new List<GameObject>());

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
        player.TryGetComponent(out Animator pAnimator);
        shield.TryGetComponent(out Animator sAnimator);
        playerAnimator = pAnimator;
        shieldAnimator = sAnimator;
        bulletsPosition = bullets.transform.position;

        buttonScriptThis = this;


        currentAdvertisement = Instantiate(advertisementPrefab, referenceObject2.transform);
        currentAdvertisement.TryGetComponent(out SpriteRenderer currentSprite);
        currentSprite.sprite = GetAdvertisement();
        publicPlayer = player;
    }
    Sprite GetAdvertisement()
    {
        return (new List<Sprite> { advertisement1, advertisement2, advertisement3 })[UnityEngine.Random.Range(0, 2)];
    }
    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0) return;
        if (win) return;

        if (!currentAdvertisement.activeSelf)
        {
            currentAdvertisement = Instantiate(advertisementPrefab, referenceObject2.transform);
            currentAdvertisement.TryGetComponent(out SpriteRenderer currentSprite);
            currentSprite.sprite = GetAdvertisement();
        }

        if (lastClicked == 10) return;

        Transform firstChild;
        List<GameObject> currentList = commandDict[lastClicked];
        try
        {
            firstChild = currentList[0].transform.GetChild(0);
        }
        catch // There are no more objects
        {
            switch (lastClicked)
            {
                case 0:
                    OnAttack();
                    break;
                case 1:
                    OnDefense();
                    break;
                case 2:
                    OnDodge();
                    break;
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
        OnDamage();
    }
    public void SelectAction(int objectId)
    {
        if (lastClicked == objectId) return;

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
        Sprite currSprite = GetSprite(objectId);

        DestroyObjects();
        int keyAmount = UnityEngine.Random.Range(minAmount, maxAmount);
        for (int i = 0; i < keyAmount; i++)
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
    public Sprite GetSprite(int? objectId = null)
    {
        if (objectId == null)
        {
            objectId = lastClicked;
        }
        switch (objectId)
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
        for (int i = 0; i < buttons.Count; i++)
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
    private void ActivateChild(GameObject parentObject)
    {
        if (codeId == 9)
        {
            DeactivateRange(0, 9);
        }
        else if (codeId == 13)
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
    public void OnDefense()
    {
        if (shieldIsActive) return;
        shieldAnimator.SetBool("hasShield", true);
        shieldIsActive = true;
    }
    private void OnDodge()
    {
        isInDodge = true;
        StartCoroutine(ResetAttack(0));
        RemoveShield("VanishShield");
        playerAnimator.SetTrigger("Dodge");
        shield.SetActive(false);
        commandDict[0] = new List<GameObject>();
        commandDict[1] = new List<GameObject>();
        CreateKeys(0);
        CreateKeys(1);
        shield.SetActive(true);
        StartCoroutine(ResetDodge());
    }
    IEnumerator ResetDodge()
    {
        yield return new WaitForSeconds(4);
        isInDodge = false;
    }
    public void BreakShield()
    {
        RemoveShield("BreakShield");
    }
    public void RemoveShield(string triggerName)
    {
        shieldAnimator.SetBool("hasShield", false);
        shieldAnimator.SetTrigger(triggerName);
        shieldIsActive = false;
    }
    public void OnDamage(int corruption = 1)
    {
        if (UnityEngine.Random.value < 0.1f) Instantiate(popUp, UnityEngine.Random.insideUnitCircle * 6, Quaternion.identity);
        PlayerMap.Corruption += corruption;
        if (PlayerMap.Corruption >= 100)
        {
            StartCoroutine(Crash());
        }
        playerAnimator.SetTrigger("Damage");
        player.transform.GetChild(0).TryGetComponent(out TMP_Text u);
        u.text = "Corruption: " + PlayerMap.Corruption;
    }

    // I am very sorry for doing this, but I have no election left, time is over
    // By the time I wrote this, only god & I knew what I was doing, now only god knows
    private void OnAttack()
    {
        bullets.SetActive(true);
        StartCoroutine(ResetAttack());
        enemyHealth--;
        enemy.transform.GetChild(0).TryGetComponent(out TMP_Text u);
        u.text = (enemyHealth * 10) + "%";
        if (enemyHealth <= 0)
        {
            win = true;
            winPanel.SetActive(true);
        }
    }

    IEnumerator ResetAttack(int t = 3)
    {
        yield return new WaitForSeconds(t);
        bullets.SetActive(false);
        bullets.transform.position = bulletsPosition;
    }
    IEnumerator Crash()
    {
        EndPanel.SetActive(true);
        yield return new WaitForSecondsRealtime(5);
        Application.Quit();
    }
    public void ButtonClickHandler()
    {
        PlayerMap.level2 = true;
        SceneLoader.nextScene = 2;
        SceneManager.LoadScene(1);
    }
}