using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailEat : MonoBehaviour
{
    public GameObject[] snailFood;
    public GameObject whoHitMe;
    public Vector3 amountToGrow = new Vector3(0.5f, 0.5f, 0.5f);
    public float foodCounter = 0;
    public float numPlantsToGrowth = 1;

    // Start is called before the first frame update
    void Start()
    {
        snailFood = GameObject.FindGameObjectsWithTag("SnailFood");
    }

    private void OnTriggerEnter(Collider collider)
    {
        whoHitMe = collider.gameObject;
        foreach (GameObject food in snailFood)
        {
            if (food == whoHitMe)
            {
                Debug.Log("Eating...");
                Destroy(food);

                foodCounter++;
                if(foodCounter == numPlantsToGrowth)
                {
                    Grow(amountToGrow);
                }
            }
        }
    }

    private void Grow(Vector3 amountToGrow)
    {
        Debug.Log("Growing...");
        transform.localScale += amountToGrow;
        foodCounter = 0;
        numPlantsToGrowth += 2;
    }
}
