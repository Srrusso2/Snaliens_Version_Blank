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
        
    }

    void OnTriggerEnter(Collider collider){
        objectInCollider=collider.gameObject;
        
    }

    void OnTriggerStay(Collider collider){

    }
}
