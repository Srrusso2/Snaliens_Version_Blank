using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailEat : MonoBehaviour
{
    public GameObject[] snailFood;
    public GAME_MANAGER gm;
    public GameObject whoHitMe;

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
            }
        }
    }
}
