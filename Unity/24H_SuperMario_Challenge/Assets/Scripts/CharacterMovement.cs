using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    public float horMovementSpeed = 15f;
    public float horMovementSpeedAirMultiplier = 1.4f;
    public float horMovementSpeedShiftMultiplier = 1.8f;
    public float jumpSpeed = 100f;
    public float jumpSpeedShiftMultiplier = 1.2f;
    public float gravity = 1.0f;

    private Vector3 currentMovementVector = Vector3.zero;
    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        var shiftIsPressed = Input.GetButton("Shift");

        if (controller.isGrounded)
        {
            //Jumping
            if (Input.GetButtonDown("Jump"))
            {
                if (shiftIsPressed)
                    currentMovementVector.y = jumpSpeed * jumpSpeedShiftMultiplier;
                else
                    currentMovementVector.y = jumpSpeed;
            }
                
        }

        //Hor Movement.
        currentMovementVector.x = Input.GetAxis("Horizontal") * horMovementSpeed;

        if (shiftIsPressed)
            currentMovementVector.x *= horMovementSpeedShiftMultiplier;

        if (!controller.isGrounded)
            currentMovementVector.x *= horMovementSpeedAirMultiplier;


        // Apply Gravity 
        currentMovementVector.y -= gravity;

        controller.Move(currentMovementVector * Time.deltaTime);
    }


}
