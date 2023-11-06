using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public GAME_MANAGER gm;
    public TMP_Text startScreen;
    public TMP_Text pauseScreen;
    public TMP_Text growText;
    public TMP_Text snailienHidingText;
    public RawImage loseScreen;
    public RawImage enemyWarning;

    private float warningTimer;

    void Start()
    {
        switchCursorState(false);
        startScreen.gameObject.SetActive(true);
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

    public void startGame()
    {
        switchCursorState(true);
        startScreen.gameObject.SetActive(false);
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
