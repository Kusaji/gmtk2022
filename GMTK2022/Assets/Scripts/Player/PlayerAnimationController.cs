using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KinematicCharacterController;
using KinematicCharacterController.Examples;

public class PlayerAnimationController : MonoBehaviour
{
    [Header("Inputs")]
    public float horizontalInput;
    public float verticalInput;
    public float leftClickInput;
    public float jumpInput;

    [Header("Components")]
    public PlayerComponents components;
    public Animator playerAnimator;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (components.health.isAlive)
        {
            HandlePlayerInput();
            PlayerAnimationTriggers();
        }
    }

    void PlayerAnimationTriggers()
    {
        //playerAnimator.SetFloat("Horizontal", horizontalInput);
        //playerAnimator.SetFloat("Vertical", verticalInput);


        if (Input.GetKey(KeyCode.W))
        {
            playerAnimator.SetFloat("Horizontal", 0);
            playerAnimator.SetFloat("Vertical", 1);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            playerAnimator.SetFloat("Horizontal", 0);
            playerAnimator.SetFloat("Vertical", -1);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            playerAnimator.SetFloat("Horizontal", -1);
            playerAnimator.SetFloat("Vertical", 0);
        }

        else if (Input.GetKey(KeyCode.D))
        {
            playerAnimator.SetFloat("Horizontal", 1);
            playerAnimator.SetFloat("Vertical", 0);
        }
        else
        {
            playerAnimator.SetFloat("Horizontal", 0);
            playerAnimator.SetFloat("Vertical", 0);
        }
    }

    void HandlePlayerInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        leftClickInput = Input.GetAxis("Fire1");
        jumpInput = Input.GetAxis("Jump");

        if (Input.GetKeyDown(KeyCode.P))
        {
            WarpToSpawn();
        }
    }

    void WarpToSpawn()
    {
        GetComponent<KinematicCharacterMotor>().SetPosition(new Vector3(0, 5, 0));
    }
}

//Old Animations
/*
 *         //Run Forward
        if (verticalInput > 0)
        {
            playerAnimator.SetTrigger("RunForward");
        }

        //Backpeddle
        if (verticalInput < 0)
        {
            playerAnimator.SetTrigger("Backpeddle");
        }

        //Strafe Right
        if (horizontalInput > 0)
        {
            playerAnimator.SetTrigger("StrafeRight");
        }

        //Strafe Left
        if (horizontalInput < 0)
        {
            playerAnimator.SetTrigger("StrafeLeft");
        }

        //Jump
        if (jumpInput > 0)
        {
            playerAnimator.SetTrigger("Jump");
        }

        //Idle
        if (verticalInput == 0 && horizontalInput == 0 && jumpInput == 0)
        {
            playerAnimator.SetTrigger("Idle");
        }
*/