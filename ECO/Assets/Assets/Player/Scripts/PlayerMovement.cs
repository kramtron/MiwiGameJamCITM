using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("KeyBinds")]
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;

    [Header("Drag")]
    float groundDrag = 6f;
    float airDrag = 1.5f;

    [Header("Movement")]

    public float movementSpeed = 6f;
    public float movementMultiplyer = 10f;

    float horizontalMovement;
    float verticalMovement;
    [SerializeField] float dashForce = 1500;
    [SerializeField] float jumpForce = 2.5f;
    [SerializeField] float airMovMultiplier = 0.2f;

    Vector3 moveDirection;
    Vector3 dashForceVector;
    Rigidbody player;
    
    [SerializeField] private KeyCode runKey;
    private bool dash;
    public Camera cam;

    private float timeSinceLastDash=2f;
    private float timeBwDash = 1f;

    private float lastMovement;

    private bool isGrounded;

    private float playerHeight;
    private CapsuleCollider playerCollider;


    [SerializeField] AudioSource dashSound;
    [SerializeField] AudioSource jumpSound;

    [SerializeField] Animator playerAnim;
    private void Start()
    {
        player = GetComponent<Rigidbody>();
        player.freezeRotation = true;
        playerCollider = GetComponent<CapsuleCollider>();
    }
    private bool CanDash() => timeSinceLastDash > timeBwDash;
    private void Update()
    {
        timeSinceLastDash += Time.deltaTime;
        

        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerCollider.height / 2 + 0.1f);
        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            jump();
            playerAnim.SetBool("Jump", true);
        }
        else
        {
            playerAnim.SetBool("Jump", false);

        }
        playerAnim.SetBool("DashAdelante", false);
        playerAnim.SetBool("DashAtras", false);

        ControlDrag();
        PlayerInput();


        LastMove();
    }

    private void LastMove()
    {
        if (horizontalMovement != 0)
        {
            lastMovement = horizontalMovement;
        }
        else
        {
            playerAnim.SetBool("Adelante", false);
            playerAnim.SetBool("Atras", false);

        }
    }

    private void PlayerInput()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");

        if (CanDash())
        {
            Dash();
        }
        
        //Se mueve a donde mira
        moveDirection = transform.right * horizontalMovement;
    }

    private void FixedUpdate()
    {
        PlayerMove();   
    }

    private void PlayerMove()
    {
        if(isGrounded)
        {
            if (Input.GetKey(runKey))
            {
                player.AddForce(moveDirection.normalized * movementSpeed * movementMultiplyer * 2f, ForceMode.Acceleration);
            }
            else
            {
                player.AddForce(moveDirection.normalized * movementSpeed * movementMultiplyer, ForceMode.Acceleration);
            }
            //playerAnim.SetBool("Adelante", true);
        }
        else
        {
            if (Input.GetKey(runKey))
            {
                player.AddForce(moveDirection.normalized * movementSpeed * movementMultiplyer * 2f * airMovMultiplier, ForceMode.Acceleration);
            }
            else
            {
                player.AddForce(moveDirection.normalized * movementSpeed * movementMultiplyer * airMovMultiplier, ForceMode.Acceleration);
            }
            

        }
        if (lastMovement <= -1) 
        {
            playerAnim.SetBool("Atras", true);
        }
        else
        {
            playerAnim.SetBool("Adelante", true);
        }
    }

    private void jump()
    {
        player.AddForce(transform.up * jumpForce * 1000f, ForceMode.Impulse);
        jumpSound.Play();
    }

    private void ControlDrag()
    {
        if(isGrounded)
        {
            player.drag = groundDrag;
        }
        else
        {
            player.drag = airDrag;
        }
    }

    private void Dash()
    {
        dashForceVector = transform.right * dashForce * 100;

        

        if (Input.GetMouseButtonDown(1))
        {
            if (lastMovement <= -1)
            {
                if (isGrounded)
                {
                    player.AddForce(-dashForceVector, ForceMode.Impulse);
                }
                else
                {
                    player.AddForce(-dashForceVector * airMovMultiplier * 2.2f, ForceMode.Impulse);
                }
            playerAnim.SetBool("DashAtras", true);

            }
            else
            {
                if (isGrounded)
                {
                    player.AddForce(dashForceVector, ForceMode.Impulse);
                }
                else
                {
                    player.AddForce(dashForceVector * airMovMultiplier * 2.2f, ForceMode.Impulse);
                }
                playerAnim.SetBool("DashAdelante", true);


            }
            dashSound.Play();
            timeSinceLastDash = 0;

        }
    }
}
