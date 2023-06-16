using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [Header("Movement")]

    public float movementSpeed = 6f;
    public float movementMultiplyer = 10f;

    float horizontalMovement;
    float verticalMovement;
    [SerializeField] float dashForce = 1500;

    Vector3 moveDirection;
    Vector3 dashForceVector;
    Rigidbody player;
    
    [SerializeField] private KeyCode runKey;
    private bool dash;
    public Camera cam;
    private void Start()
    {
        player = GetComponent<Rigidbody>();
        player.freezeRotation = true;
    }
    private void Update()
    {
        PlayerInput();
    }

    private void PlayerInput()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = 0;

        dashForceVector = transform.right * dashForce * 100;

        if (Input.GetMouseButtonDown(1))
        {
            
            player.AddForce(dashForceVector, ForceMode.Impulse);

        }
        

        //Se mueve a donde mira
        moveDirection = transform.forward * verticalMovement + transform.right * horizontalMovement;
    }

    private void FixedUpdate()
    {
        PlayerMove();   
    }

    private void PlayerMove()
    {

        if (Input.GetKey(runKey))
        {
            player.AddForce(moveDirection.normalized * movementSpeed * movementMultiplyer * 2f, ForceMode.Acceleration);
        }
        else
        {
            player.AddForce(moveDirection.normalized * movementSpeed * movementMultiplyer, ForceMode.Acceleration);
        }

        
    }
}
