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
    public Vector3 amountToGrow = new Vector3(0.2f, 0.3f, 0.5f);
    public float foodCounter = 0;
    public float numPlantsToGrowth = 10;
    public float speedCap = 50f;
    public float speedIncrement = 0.5f;
    public int level = 1;
    public bool snailienHiding = false;
    public bool hasHideAbility = false;
    public bool hasSprintAbility = false;
    public AudioClip eatingSound;
    public AudioClip warningSound;
    
    void Start(){
        snailFood = GameObject.FindGameObjectsWithTag("SnailFood");
        //source.volume=2;
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Space) && gm.gameActive && hasHideAbility)
        {
            if(snailienHiding)
            {
                Hide(false);
            }
            else
            {
                Hide(true);
            }
        }
        
        if((Input.GetKeyDown(KeyCode.LeftShift)) && gm.gameActive && !snailienHiding && hasSprintAbility)
        {
            GetComponent<SnailMovement>().Sprint();
        }

        if((Input.GetKeyUp(KeyCode.LeftShift)) && gm.gameActive && !snailienHiding && hasSprintAbility)
        {
            GetComponent<SnailMovement>().MoveSpeed /= GetComponent<SnailMovement>().sprintMultiplier;
        }

    }

    private void OnTriggerEnter(Collider collider){
        whoHitMe = collider.gameObject;
        foreach (GameObject food in snailFood)
        {
            if (food == whoHitMe)
            {
                Debug.Log("Eating...");
                AudioSource.PlayClipAtPoint(eatingSound,transform.position);
                Destroy(food);

                foodCounter++;
                if(foodCounter == numPlantsToGrowth)
                {
                    Grow(amountToGrow);
                }
            }
        }
    }

    private void Grow(Vector3 amountToGrow){
        Debug.Log("Growing...");
        transform.localScale += amountToGrow;
        foodCounter = 0;
        level++;
        numPlantsToGrowth += 20;
        GetComponent<SnailMovement>().IncreaseBaseMoveSpeed(speedIncrement);
        if (level == 2)
        {
            gm.uiManager.levelUp(level);
        }
    }

    private void Hide(bool isHiding){
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
}
