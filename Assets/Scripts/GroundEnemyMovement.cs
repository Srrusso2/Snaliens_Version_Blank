using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemyMovement : MonoBehaviour{
    public float enemySpeed;
    public float enemySpeedReg; //speed for when the enemy is regularly moving
    public float enemySpeedHungry; //speed for when the enemy is charging at the player
    public float enemyRotationY;
    public float enemySightRange; //range of raycast for detecting snails
    public GAME_MANAGER gm;
    public int maxSnailienEatLevel;
    public bool canWormAttack=true;
    public Vector3 enemyStartPos;
    public bool hasAttacked=false;
    public GameObject cone;
    public EnemySightCode esc;
    public int growthPoints;
    //public Vector3 right45 = (transform.forward + transform.right).normalized;
    //ublic Vector3 left45 = (transform.forward - transform.right).normalized;

    void Start(){
        enemyStartPos = gameObject.transform.position;
        cone = gameObject.transform.GetChild(1).gameObject;
        esc = cone.GetComponent<EnemySightCode>();
    }

    void Update(){
        if(gm.gameActive==true){
            RaycastHit hit;
            //RaycastHit hit2;
            //RaycastHit hit3;
            //Physics.Raycast(transform.position, transform.TransformDirection(right45);
            // making it so that when a raycast is cast forward from the enemy a code checks if it has contacted the player through use of GetComponent<playerScript> 
            //(or something like that) but it does that in a certain range and if it finds an object with that script it goes directly toawrds it
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit)){
                Debug.DrawLine(transform.position + transform.TransformDirection(Vector3.forward), hit.point, Color.cyan);
                Debug.DrawLine(transform.position + transform.TransformDirection(transform.forward + transform.right).normalized, hit.point, Color.cyan);
                Debug.DrawLine(transform.position + transform.TransformDirection(transform.forward - transform.right).normalized, hit.point, Color.cyan);
                if(esc.objectInCollider.tag!="Player"||hit.collider.GetComponent<SnailMovement>()==null||hit.distance>enemySightRange||gm.snailienManager.snailienHiding||gm.snailienManager.level>=maxSnailienEatLevel){
                    transform.Translate(Vector3.forward * enemySpeed * Time.deltaTime);
                    transform.Rotate(0.0f,enemyRotationY,0.0f, Space.Self);
                }else{
                    enemySpeed=enemySpeedHungry;
                    transform.Translate(Vector3.forward * enemySpeed * Time.deltaTime);
                    gm.uiManager.showWarning();
                    AudioSource.PlayClipAtPoint(gm.snailienManager.warningSound,gm.snailienManager.transform.position);
                    //canWormAttack=true;
                }
            }
            if(esc.objectInCollider.tag=="Player"){
                //if(esc.objectInCollider.){

                //}
            }
            if(gm.snailienManager.level>maxSnailienEatLevel){
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
