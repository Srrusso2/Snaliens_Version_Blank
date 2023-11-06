using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public GAME_MANAGER gm;
    public TMP_Text levelUpScreen;
    public TMP_Text pauseScreen;
    public TMP_Text growText;
    public TMP_Text snailienHidingText;
    public TMP_Text levelText;
    public RawImage loseScreen;
    public RawImage enemyWarning;
    public List<GameObject> abilities = new List<GameObject>();

    private float warningTimer;

    void Start()
    {
        switchCursorState(true);
        abilities = new List<GameObject>();

        levelUpScreen.gameObject.SetActive(true);

        foreach(GameObject ability in GameObject.FindGameObjectsWithTag("Level2Ability"))
        {
            abilities.Add(ability);
        }

        foreach (GameObject ability in GameObject.FindGameObjectsWithTag("OtherAbilities"))
        {
            abilities.Add(ability);
        }

        levelUpScreen.gameObject.SetActive(false);
        growText.gameObject.SetActive(true);
        levelText.gameObject.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (gm.gameActive)
            {
                switchCursorState(false);
                pauseScreen.gameObject.SetActive(true);
                gm.gameActive = false;
                growText.gameObject.SetActive(false);
                levelText.gameObject.SetActive(false);
                if (snailienHidingText.gameObject.activeSelf)
                {
                    snailienHidingText.gameObject.SetActive(false);
                }
            }
            else
            {
                switchCursorState(true);
                if (pauseScreen.gameObject.activeSelf)
                {
                    pauseScreen.gameObject.SetActive(false);
                    gm.gameActive = true;
                    growText.gameObject.SetActive(true);
                    levelText.gameObject.SetActive(true);
                }
                else
                {
                    loseScreen.gameObject.SetActive(false);
                    gm.gameActive = true;
                    growText.gameObject.SetActive(true);
                    levelText.gameObject.SetActive(true);
                    Application.LoadLevel(Application.loadedLevel);
                }
            }
        }

        warningTimer -= Time.deltaTime;
        if(warningTimer <= 0)
        {
            enemyWarning.gameObject.SetActive(false);
        }

        if (gm.gameActive == false)
        {
            Rigidbody rb = gm.snailienManager.GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezePosition;
        }
        else
        {
            Rigidbody rb = gm.snailienManager.GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.None;
        }
    }

    public void showWarning()
    {
        warningTimer = 1f;
        if(!enemyWarning.gameObject.activeSelf)
        {
            enemyWarning.gameObject.SetActive(true);
        }
    }

    public void levelUp(int level)
    {
        switchCursorState(false);
        gm.gameActive = false;
        levelUpScreen.gameObject.SetActive(true);
        levelText.SetText("Level: " + gm.snailienManager.level);
        growText.gameObject.SetActive(false);
        levelText.gameObject.SetActive(false);
        if (level == 2)
        {
            foreach(GameObject ability in abilities)
            {
                if(ability.CompareTag("Level2Ability"))
                {
                    ability.gameObject.SetActive(true);
                }
                else
                {
                    ability.gameObject.SetActive(false);
                }
            }
        }
        else
        {
            foreach(GameObject ability in abilities)
            {
                if(ability.CompareTag("OtherAbilities"))
                {
                    ability.gameObject.SetActive(true);
                }
                else
                {
                    ability.gameObject.SetActive(false);
                }
            }
        }
    }

    public void continueGameFromLevelUp()
    {
        switchCursorState(true);
        levelUpScreen.gameObject.SetActive(false);
        gm.gameActive = true;
        growText.gameObject.SetActive(true);
        levelText.gameObject.SetActive(true);
    }

    public void switchCursorState(bool locked)
    {
        if(locked)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
    }

    public void changeGrowText(float numPlants)
    {
        growText.SetText("Number of Plants Until Next Growth: " + numPlants);
    }

    public void changeHidingText(bool hiding)
    {
        if(hiding)
        {
            snailienHidingText.gameObject.SetActive(true);
        }
        else
        {
            snailienHidingText.gameObject.SetActive(false);
        }
    }
}
