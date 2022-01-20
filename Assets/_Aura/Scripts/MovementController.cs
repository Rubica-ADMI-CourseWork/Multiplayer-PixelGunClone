using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    #region Fields
    Rigidbody rb;
    private float lookUpDownValue;
    private float currentCameraLookValue;
    Vector3 velocity = Vector3.zero;
    Vector3 rotation = Vector3.zero;

    [Header("Movement/Look properties")]
    [SerializeField] float movementSpeed;
    [SerializeField] float rotationSensitivity;
    [SerializeField] float lookSensitivity;
    [Header("Camera as PC Eyes")]
    [SerializeField] Camera eyes;

    #endregion

    #region Unity Callbacks
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        //read input for forward back side to side movement
        var horizontalMovement = Input.GetAxis("Horizontal");
        var verticalMovement = Input.GetAxis("Vertical");

        var forwardMovement = transform.forward * verticalMovement;
        var sideMovement = transform.right * horizontalMovement;

        var movement = (forwardMovement + sideMovement).normalized * movementSpeed;

        //set the velocity here
        Move(movement);

        //get input for sideways rotation of body here
        var rotationInput = Input.GetAxis("Mouse X");
        var bodyRot = new Vector3(0f, rotationInput , 0f)* rotationSensitivity;
        RotatePlayer(bodyRot);


        //get input for up down rotation of camera here
        var lookUpDown = Input.GetAxis("Mouse Y") * lookSensitivity;
        PlayerLookUpDown(lookUpDown);


    }
    private void FixedUpdate()
    {
        if (velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }

        rb.MoveRotation(Quaternion.Euler(rotation));

        eyes.transform.localEulerAngles = new Vector3(currentCameraLookValue, 0f, 0f);

    }
    #endregion

    #region Private Methods

    /// <summary>
    /// Updates PC velocity based on Input from keyboard/joystick and movement speed.
    /// </summary>
    /// <param name="movement"></param>
    private void Move(Vector3 movement)
    {
     velocity = movement;
    }

    /// <summary>
    /// Updates PC Head X Axis Rotation based on Mouse Y axis Input(looking up and down)Clamped to 
    /// min and max head rotation values
    /// </summary>
    /// <param name="lookUpDown"></param>
    private void PlayerLookUpDown(float lookUpDown)
    {
        currentCameraLookValue -= lookUpDown;
        currentCameraLookValue = Mathf.Clamp(currentCameraLookValue, -85, 80f);
    }

    /// <summary>
    /// Updates PC body rotation based in input and rotation speed
    /// </summary>
    /// <param name="bodyRot"></param>
    private void RotatePlayer(Vector3 bodyRot)
    {
        rotation += bodyRot;
    }

    #endregion 

}
