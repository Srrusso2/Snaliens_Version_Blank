using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailMovement : MonoBehaviour
{
    //components
    public CharacterController CC;
    public Transform CamTransform;
    public GAME_MANAGER gm;
    public Rigidbody rb;

    //floats
    public float MoveSpeed = 10;
    public float currentBaseSpeed = 10;
    public float MouseSensitivity;
    private float camRotation = 0f;
    public float gravity = -9.8f;
    public float yVelocity = 0f;
    public float sprintMultiplier = 3.0f;
    
    // Update is called once per frame
    void Update()
    {
        Vector3 movement = Vector3.zero;
        if (gm.gameActive)
        {
            if (!gm.snailienManager.snailienHiding)
            {
                // X/Z movement
                float forwardMovement = Input.GetAxis("Vertical") * MoveSpeed * Time.deltaTime;
                float sideMovement = Input.GetAxis("Horizontal") * MoveSpeed * Time.deltaTime;
                yVelocity += gravity *= Time.deltaTime;

                movement += (transform.forward * forwardMovement) + (transform.right * sideMovement) + (transform.up * yVelocity);
            }
            else
            {
                // X/Z movement
                float forwardMovement = Input.GetAxis("Vertical") * MoveSpeed/4 * Time.deltaTime;
                float sideMovement = Input.GetAxis("Horizontal") * MoveSpeed/4 * Time.deltaTime;
                yVelocity += gravity *= Time.deltaTime;

                movement += (transform.forward * forwardMovement) + (transform.right * sideMovement) + (transform.up * yVelocity);
            }
            CC.Move(movement);

            //cam movement
            float mouseInputY = Input.GetAxis("Mouse Y") * MouseSensitivity;
            camRotation -= mouseInputY;
            camRotation = Mathf.Clamp(camRotation, 9f, 45f);
            CamTransform.localRotation = Quaternion.Euler(new Vector3(camRotation, 0f, 0f));

            float mouseInputX = Input.GetAxis("Mouse X") * MouseSensitivity;
            transform.rotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0f, mouseInputX, 0f));
        }
        else
        {
            float forwardMovement = 0;
            float sideMovement = 0;
            yVelocity += gravity *= Time.deltaTime;

            movement += (transform.forward * forwardMovement) + (transform.right * sideMovement) + (transform.up * yVelocity);

            CC.Move(movement);
        }
    }

    public void IncreaseBaseMoveSpeed(float multiplier)
    {
        if(MoveSpeed < gm.snailienManager.speedCap)
        {
            MoveSpeed *= multiplier;
        }

        currentBaseSpeed = MoveSpeed;
    }

    public void Sprint()
    {
        MoveSpeed *= sprintMultiplier;
    }

    public void endSprint()
    {
        MoveSpeed = currentBaseSpeed;
    }
}
