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

    public Vector3 growthAmount = new Vector3(0.1f, 0.15f, 0.25f);
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
                gm.uiManager.setShowTipText("Hiding in Shell");
            }
        }
        
        if((Input.GetKeyDown(KeyCode.LeftShift)) && gm.gameActive && !snailienHiding && hasSprintAbility)
        {
            abilityTimer = abilityTimeLimit;
            gm.uiManager.setShowTipText("Sprinting");
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

                if (food.GetComponent<TallFlower>() && level >= food.GetComponent<TallFlower>().level)
                {
                    TallFlower tallFlower = food.GetComponent<TallFlower>();
                    foodCounter += tallFlower.growthPoints;
                    Grow(tallFlower.growthPoints);
                    Destroy(food);
                }
                else if(food.GetComponent<SmallFlower>() && level >= food.GetComponent<SmallFlower>().level)
                {
                    SmallFlower smallFlower = food.GetComponent<SmallFlower>();
                    foodCounter += smallFlower.growthPoints;
                    Grow(smallFlower.growthPoints);
                    Destroy(food);
                }
                else if(food.GetComponent<BulbTree>() && level >= food.GetComponent<BulbTree>().level)
                {
                    BulbTree bulbTree = food.GetComponent<BulbTree>();
                    foodCounter += bulbTree.growthPoints;
                    Grow(bulbTree.growthPoints);
                    Destroy(food);
                }
                else if(food.GetComponent<Mushroom>() && level >= food.GetComponent<Mushroom>().level)
                {
                    Mushroom mushroom = food.GetComponent<Mushroom>();
                    foodCounter += mushroom.growthPoints;
                    Grow(mushroom.growthPoints);
                    Destroy(food);
                }
                else if(food.GetComponent<GroundEnemyMovement>())
                {
                    GroundEnemyMovement enemy = food.GetComponent<GroundEnemyMovement>();
                    foodCounter += enemy.growthPoints;
                    Grow(enemy.growthPoints);
                    Destroy(food);
                }
                else
                {
                    gm.uiManager.setShowTipText("Grow bigger first!");
                }

                if (foodCounter >= numPlantsToGrowth)
                {
                    LevelUp();
                }
            }
        }
    }

    private void Grow(int growthMultiplier)
    {
        growthAmount *= growthMultiplier;
        transform.localScale += growthAmount;
        growthAmount /= growthMultiplier;
    }

    private void LevelUp()
    {
        foodCounter -= numPlantsToGrowth;
        level++;
        numPlantsToGrowth += 20;
        GetComponent<SnailMovement>().IncreaseBaseMoveSpeed(speedMultiplier);
        gm.uiManager.levelUp(level);
    }

    private void Hide(bool isHiding)
    {
        if(isHiding)
        {
            snailienShell.SetActive(true);
            snailien.SetActive(false);
        }
        else
        {
            snailien.SetActive(true);
            snailienShell.SetActive(false);
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
        gm.uiManager.gm.uiManager.setShowTipText("Press Space to Hide");
    }

    public void gainSprintAbility()
    {
        hasSprintAbility = true;
        gm.uiManager.continueGameFromLevelUp();
        gm.uiManager.gm.uiManager.setShowTipText("Press Shift to Sprint");
    }

    public void shortenCoolDown()
    {
        coolDownLength /= 2;
        gm.uiManager.continueGameFromLevelUp();
    }
}
