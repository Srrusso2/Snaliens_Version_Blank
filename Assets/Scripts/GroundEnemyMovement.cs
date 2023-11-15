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
    public Vector3 right45;
    public Vector3 left45;
    public bool hits1=false,hits2=false,hits3=false;

    void Start(){
        enemyStartPos = gameObject.transform.position;
        cone = gameObject.transform.GetChild(1).gameObject;
        esc = cone.GetComponent<EnemySightCode>();
        right45=(Vector3.forward + Vector3.right).normalized;
        left45=(Vector3.forward - Vector3.right).normalized;
    }

    void Update(){
        if(gm.gameActive==true){
            RaycastHit hit;
            RaycastHit hit2;
            RaycastHit hit3;

            if (Physics.Raycast(transform.position, transform.TransformDirection(right45), out hit2)){
                Debug.DrawLine(transform.position + transform.TransformDirection(right45), hit2.point, Color.cyan);
                if(hit2.collider.GetComponent<SnailMovement>()!=null){
                    hits2=true;
                    hits3=false;
                }
            }
            if (Physics.Raycast(transform.position, transform.TransformDirection(left45), out hit3)){
                Debug.DrawLine(transform.position + transform.TransformDirection(left45), hit3.point, Color.cyan);
                if(hit3.collider.GetComponent<SnailMovement>()!=null){
                    hits3=true;
                    hits2=false;
                }
            }

            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit)){
                Debug.DrawLine(transform.position + transform.TransformDirection(Vector3.forward), hit.point, Color.cyan);
                if(hit.collider.GetComponent<SnailMovement>()!=null){
                    hits1=true;
                }
            }

            //Code that checks if the snailien is attackable
            if(esc.objectInCollider.tag!="Player"||hit.collider.GetComponent<SnailMovement>()==null||hit.distance>enemySightRange||gm.snailienManager.snailienHiding||gm.snailienManager.level>=maxSnailienEatLevel){
                transform.Translate(Vector3.forward * enemySpeed * Time.deltaTime);
                transform.Rotate(0.0f,enemyRotationY,0.0f, Space.Self);
                canWormAttack=false;
            }else{
                //enemySpeed=enemySpeedHungry;
                //transform.Translate(Vector3.forward * enemySpeed * Time.deltaTime);
                //gm.uiManager.showWarning();
                //AudioSource.PlayClipAtPoint(gm.snailienManager.warningSound,gm.snailienManager.transform.position);
            }

            if(esc.objectInCollider.tag=="Player"){
                if(hits2==true){
                    
                }else if(hits3==true){
                    transform.Rotate(0.0f,-enemyRotationY*2,0.0f, Space.Self);
                }
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
