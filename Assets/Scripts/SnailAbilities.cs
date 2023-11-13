using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailAbilities : MonoBehaviour
{
    public GameObject[] snailFood;
    public GameObject whoHitMe;
    public GameObject snailien;
    public GameObject snailienShell;
    public GAME_MANAGER gm;

    public float growthMultiplier = 2;
    public float foodCounter = 0;
    public float numPlantsToGrowth = 10;
    public float speedCap = 50f;
    public float speedMultiplier = 2.0f;
    public int level = 1;

    public float coolDownTimer = -1;
    public float coolDownLength = 7;
    public float abilityTimeLimit = 3;
    public float abilityTimer = -1;

    public string cooledDownAbility = "";

    public bool snailienHiding = false;
    public bool hasHideAbility = false;
    public bool hasSprintAbility = false;

    public AudioClip eatingSound;
    public AudioClip warningSound;
    
    void Start(){
        snailFood = GameObject.FindGameObjectsWithTag("SnailFood");
        gm.uiManager.changeGrowText(numPlantsToGrowth - foodCounter);
        //source.volume=2;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && gm.gameActive && hasHideAbility)
        {
            if(snailienHiding)
            {
                Hide(false);
                startCoolDown(coolDownLength, "Hide");
            }
            else
            {
                Hide(true);
            }
        }
        
        if((Input.GetKeyDown(KeyCode.LeftShift)) && gm.gameActive && !snailienHiding && hasSprintAbility)
        {
            abilityTimer = abilityTimeLimit;
            GetComponent<SnailMovement>().Sprint();
        }

        //TIMERS
        coolDownTimer -= Time.deltaTime;
        if (coolDownTimer <= 0)
        {
            endCoolDown(cooledDownAbility);
        }

        abilityTimer -= Time.deltaTime;
        if (abilityTimer <= 0 && abilityTimer > -1)
        {
            GetComponent<SnailMovement>().endSprint();
            startCoolDown(coolDownLength, "Sprint");
        }
        Debug.Log(abilityTimer);

        gm.uiManager.changeGrowText(numPlantsToGrowth - foodCounter);

    }

    private void OnTriggerEnter(Collider collider)
    {
        whoHitMe = collider.gameObject;
        foreach (GameObject food in snailFood)
        {
            if (food == whoHitMe && !snailienHiding)
            {
                AudioSource.PlayClipAtPoint(eatingSound,transform.position);
                Destroy(food);

                foodCounter++;
                if(foodCounter == numPlantsToGrowth)
                {
                    Grow();
                }
            }
        }
    }

    private void Grow()
    {
        transform.localScale *= growthMultiplier;
        foodCounter = 0;
        level++;
        numPlantsToGrowth += 20;
        gm.uiManager.changeGrowText(numPlantsToGrowth - foodCounter);
        GetComponent<SnailMovement>().IncreaseBaseMoveSpeed(speedMultiplier);
        gm.uiManager.levelUp(level);
    }

    private void Hide(bool isHiding)
    {
        if(isHiding)
        {
            snailienShell.SetActive(true);
            snailien.SetActive(false);
            gm.uiManager.changeHidingText(true);
        }
        else
        {
            snailien.SetActive(true);
            snailienShell.SetActive(false);
            gm.uiManager.changeHidingText(false);
        }

        snailienHiding = isHiding;
    }

    public void startCoolDown(float time, string ability)
    {
        coolDownTimer = time;
        if (ability.Equals("Sprint"))
        {
            cooledDownAbility = "Sprint";
            hasSprintAbility = false;
        }
        else if (ability.Equals("Hide"))
        {
            cooledDownAbility = "Hide";
            hasHideAbility = false;
        }

        gm.uiManager.showCoolDownText(true);
    }

    public void endCoolDown(string ability)
    {
        if (ability.Equals("Sprint"))
        {
            hasSprintAbility = true;
        }
        else if (ability.Equals("Hide"))
        {
            hasHideAbility = true;
        }

        gm.uiManager.showCoolDownText(false);
    }

    public void gainHideAbility()
    {
        hasHideAbility = true;
        gm.uiManager.continueGameFromLevelUp();
    }

    public void gainSprintAbility()
    {
        hasSprintAbility = true;
        gm.uiManager.continueGameFromLevelUp();
    }

    public void shortenCoolDown()
    {
        coolDownLength /= 2;
        gm.uiManager.continueGameFromLevelUp();
    }
}
