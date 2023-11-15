using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySightCode : MonoBehaviour{
    public GroundEnemyMovement gem;
    public bool ifHit;
    public GameObject objectInCollider;
    void Start(){
        
    }

    void Update(){
        if(objectInCollider==null){
            objectInCollider=gem.gameObject.transform.GetChild(2).gameObject;
        }
    }

    void OnTriggerEnter(Collider collider){
        objectInCollider=collider.gameObject;
        Debug.Log("Enter");
    }

    void OnTriggerStay(Collider collider){
        Debug.Log("Stayed");
    }
}
