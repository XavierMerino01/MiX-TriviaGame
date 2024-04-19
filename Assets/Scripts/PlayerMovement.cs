using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;
    private PlayerScript myPlayer;
    private Vector2 movementDirection;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    private void Awake()
    {
        myPlayer = GetComponent<PlayerScript>();
        characterController = GetComponent<CharacterController>();  
    }


    private void Update()
    {
        CheckMovement();
    }

    public void SetMovementInput(Vector2 input)
    {
        movementDirection = input;
    }

    private void CheckMovement()
    {
   
         Vector3 movement = new Vector3(movementDirection.x, 0f, movementDirection.y).normalized;
         
        if(movement.magnitude >= 0.1f && myPlayer.inputEnabled)
        {
            myPlayer.ChangePlayerState(PlayerScript.PlayerState.Walking);
            float targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            characterController.Move(movement * myPlayer.moveSpeed * Time.deltaTime);
        }
        else
        {
            myPlayer.ChangePlayerState(PlayerScript.PlayerState.Idle);
        }
    }

}

    

