using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Playables;

public class UI_Manager : MonoBehaviour
{
    public GAME_MANAGER gm;
    public TMP_Text levelUpScreen;
    public TMP_Text pauseScreen;
    public TMP_Text growText;
    public TMP_Text levelText;
    public TMP_Text coolDownText;
    public TMP_Text tipText;

    public RawImage loseScreen;
    public RawImage enemyWarning;

    public List<GameObject> abilities = new List<GameObject>();
    public PlayableDirector tipActivation;


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

        foreach (GameObject ability in GameObject.FindGameObjectsWithTag("Level3Ability"))
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

        string displayedTime = gm.snailienManager.coolDownTimer.ToString("F0");
        coolDownText.SetText(gm.snailienManager.cooledDownAbility + " Cooldown: " + displayedTime);

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

    public void setShowTipText(string text)
    {
        tipText.SetText(text);

        tipActivation.Play();
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
        else if(level == 3)
        {
            foreach (GameObject ability in abilities)
            {
                if (ability.CompareTag("Level3Ability"))
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
            levelUpScreen.SetText("YOU LEVELED UP! YAY! Sorry...we haven't made more abilities yet");
            foreach (GameObject ability in abilities)
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

    public void showCoolDownText(bool isInCoolDown)
    {
        coolDownText.gameObject.SetActive(isInCoolDown);
    }
}
