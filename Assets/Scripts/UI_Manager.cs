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

    void Start()
    {
        startScreen.gameObject.SetActive(true);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            if(gm.gameActive)
            {
                pauseScreen.gameObject.SetActive(true);
                gm.gameActive = false;
                growText.gameObject.SetActive(false);
            }
            else
            {
                if(startScreen.gameObject.activeSelf)
                {
                    startScreen.gameObject.SetActive(false);
                    gm.gameActive = true;
                    growText.gameObject.SetActive(true);
                }
                else if(pauseScreen.gameObject.activeSelf)
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
            growText.SetText("Number of Plants Until Next Growth: " + gm.snailienManager.numPlantsToGrowth);
        }

        if(gm.snailienManager.snailienHiding)
        {
            snailienHidingText.gameObject.SetActive(true);
        }
        else
        {
            snailienHidingText.gameObject.SetActive(false);
        }
    }
}
