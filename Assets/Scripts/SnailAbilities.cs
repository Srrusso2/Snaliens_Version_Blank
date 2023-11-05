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
    public Vector3 amountToGrow = new Vector3(0.5f, 0.5f, 0.5f);
    public float foodCounter = 0;
    public float numPlantsToGrowth = 1;
    public float speedCap = 50f;
    public float speedIncrement = 0.5f;
    public bool snailienHiding = false; 
    public AudioClip eatingSound;
    public AudioClip warningSound;
    
    void Start(){
        snailFood = GameObject.FindGameObjectsWithTag("SnailFood");
        //source.volume=2;
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Space) && gm.gameActive)
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
        numPlantsToGrowth += 2;
        GetComponent<SnailMovement>().increaseMoveSpeed(speedIncrement);
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
}
