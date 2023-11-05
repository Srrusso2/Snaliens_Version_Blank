using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemyMovement : MonoBehaviour{
    public float enemySpeed;
    public float enemySpeedReg; //speed for when the enemy is regularly moving
    public float enemySpeedHungry; //speed for when the enemy is charging at the player
    public float enemyRotation;
    public float enemySightRange; //range of raycast for detecting snails
    public GAME_MANAGER gm;
    public Vector3 maxSnailienEatSize;
    public bool canWormAttack=true;
    public Vector3 enemyStartPos;
    public bool hasAttacked=false;
    
    void Start(){
        enemyStartPos = gameObject.transform.position;
    }

    void Update(){
        if(gm.gameActive==true){
            RaycastHit hit;
            // making it so that when a raycast is cast forward from the enemy a code checks if it has contacted the player through use of GetComponent<playerScript> 
            //(or something like that) but it does that in a certain range and if it finds an object with that script it goes directly toawrds it
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit)){
                Debug.DrawLine(transform.position + transform.TransformDirection(Vector3.forward), hit.point, Color.cyan);
                if(hit.collider.GetComponent<SnailMovement>()==null||hit.distance>enemySightRange||gm.snailienManager.snailienHiding||gm.snailienManager.transform.localScale.y>maxSnailienEatSize.y){
                    if(hasAttacked==true){
                        transform.position = Vector3.MoveTowards(transform.position, enemyStartPos, enemySpeed*Time.deltaTime);
                        if(gameObject.transform.position==enemyStartPos){
                            hasAttacked=false;
                        }
                    }else{
                        enemySpeed=enemySpeedReg;
                        transform.Translate(Vector3.forward * enemySpeed * Time.deltaTime);
                        transform.Rotate(0.0f, enemyRotation, 0.0f, Space.Self);
                        canWormAttack=false;
                    }
                }else{
                    enemySpeed=enemySpeedHungry;
                    transform.Translate(Vector3.forward * enemySpeed * Time.deltaTime);
                    gm.uiManager.showWarning();
                    AudioSource.PlayClipAtPoint(gm.snailienManager.warningSound,gm.snailienManager.transform.position);
                    canWormAttack=true;
                    hasAttacked=true;
                }
            }
            if(gm.snailienManager.transform.localScale.y>maxSnailienEatSize.y){
                transform.gameObject.tag = "SnailFood";
                gm.snailienManager.snailFood = GameObject.FindGameObjectsWithTag("SnailFood");
            }
        }
    }

    private void OnTriggerEnter(Collider collider){
        if(canWormAttack==true){
            gm.uiManager.loseScreen.gameObject.SetActive(true);
            gm.uiManager.growText.gameObject.SetActive(false);
            gm.gameActive = false;
        }
    }

}
