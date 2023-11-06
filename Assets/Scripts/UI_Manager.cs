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
    public RawImage loseScreen;
    public RawImage enemyWarning;
    public GameObject[] abilities;

    private float warningTimer;

    void Start()
    {
        switchCursorState(true);
        List<GameObject> abilities = new List<GameObject>();
        abilities.Add(GameObject.FindGameObjectsWithTag("Level2Abilities"));
       /* abilities = GameObject.FindGameObjectsWithTag("Level2Abilities") + GameObject.FindGameObjectsWithTag("OtherAbilities");*/
        //levelUpScreen.gameObject.SetActive(true);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            if(gm.gameActive)
            {
                switchCursorState(false);
                pauseScreen.gameObject.SetActive(true);
                gm.gameActive = false;
                growText.gameObject.SetActive(false);
                if(snailienHidingText.gameObject.activeSelf)
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
                }
                else
                {
                    loseScreen.gameObject.SetActive(false);
                    gm.gameActive = true;
                    growText.gameObject.SetActive(true);
                    Application.LoadLevel(Application.loadedLevel);
                }
            }
        }

        if(gm.gameActive)
        {
            growText.SetText("Number of Plants Until Next Growth: " + (gm.snailienManager.numPlantsToGrowth - gm.snailienManager.foodCounter));
        }

        if(gm.snailienManager.snailienHiding)
        {
            snailienHidingText.gameObject.SetActive(true);
        }
        else
        {
            snailienHidingText.gameObject.SetActive(false);
        }

        warningTimer -= Time.deltaTime;
        if(warningTimer <= 0)
        {
            enemyWarning.gameObject.SetActive(false);
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
        //we will have an array of button objects tagged 'abilities'. these buttons will also have level tags, ie 'level 2'
        //we will use the level tags and level parameter to determine which buttons are active within the level up screen
    }

    public void continueGameFromLevelUp()
    {
        Debug.Log("Continue");
        switchCursorState(true);
        levelUpScreen.gameObject.SetActive(false);
        gm.gameActive = true;
        growText.gameObject.SetActive(true);
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
}
