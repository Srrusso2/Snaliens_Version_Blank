using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailMovement : MonoBehaviour
{
    //components
    public CharacterController CC;
    public Transform CamTransform;

    //floats
    public float MoveSpeed;
    public float MouseSensitivity;
    private float camRotation = 0f;
    public float gravity = -9.8f;
    public float yVelocity = 0f;

    //bools
    public bool gameActive;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        //will use this later when menu overlays are a thing
        gameActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameActive)
        {
            Vector3 movement = Vector3.zero;

            // X/Z movement
            float forwardMovement = Input.GetAxis("Vertical") * MoveSpeed * Time.deltaTime;
            float sideMovement = Input.GetAxis("Horizontal") * MoveSpeed * Time.deltaTime;
            yVelocity += gravity *= Time.deltaTime;

            movement += (transform.forward * forwardMovement) + (transform.right * sideMovement) + (transform.up * yVelocity);

            //cam movement
            float mouseInputY = Input.GetAxis("Mouse Y") * MouseSensitivity;
            camRotation -= mouseInputY;
            camRotation = Mathf.Clamp(camRotation, -90f, 90f);
            CamTransform.localRotation = Quaternion.Euler(new Vector3(camRotation, 0f, 0f));

            float mouseInputX = Input.GetAxis("Mouse X") * MouseSensitivity;
            transform.rotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0f, mouseInputX, 0f));

            CC.Move(movement);
        }
    }
}
