using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemyMovement : MonoBehaviour{
    public float enemySpeed;
    public float enemySpeedReg; //speed for when the enemy is regularly moving
    public float enemySpeedHungry; //speed for when the enemy is charging at the player
    public float enemyRotation;
    void Start(){
        
    }

    void Update(){
        RaycastHit hit;
        // making it so that when a raycast is cast forward from the enemy a code checks if it has contacted the player through use of GetComponent<playerScript> 
        //(or something like that) but it does that in a certain range and if it finds an object with that script it goes directly toawrds it
		if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit)){
            Debug.DrawLine(transform.position + transform.TransformDirection(Vector3.forward), hit.point, Color.cyan);
            Rigidbody hitRB = hit.collider.GetComponent<Rigidbody>();
			if(hit.collider.GetComponent<SnailMovement>()==null){
                enemySpeed=enemySpeedReg;
				transform.Translate(Vector3.forward * enemySpeed * Time.deltaTime);
                transform.Rotate(0.0f, enemyRotation, 0.0f, Space.Self);
			}else{
                enemySpeed=enemySpeedHungry;
                transform.Translate(Vector3.forward * enemySpeed * Time.deltaTime);
            }
		}
    }

}
