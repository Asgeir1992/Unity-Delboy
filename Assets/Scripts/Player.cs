using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour

{
    private float jumpForce = 3.0f;
    private float maxJump = 6.0f;

    private float JumpIncreased = 1f;

    private float walkingSpeed = 2.0f;
    private float maxSpeed = 5.0f;

    private float increasingSpeed = 1.2f;
    private float decreasingSpeed = 1.0f;
    private bool jumpKeyPressed;
    private bool isJumping;
    private bool jumpKeyRealesed;
    private bool isLanding;
    private bool isChangingDirection;
    private bool isStandingStill;
    
    private float horizantalInput;
    private Rigidbody rigidbodyComponent;


   [SerializeField] private Transform groundCheckTransform = null;

    // Start is called before the first frame update
    private void Start()
    {
        rigidbodyComponent = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
   private void Update()
    {

        horizantalInput = Input.GetAxis("Horizontal");

        if(Input.GetKeyDown(KeyCode.Space)){
             jumpKeyPressed = true;
        }

        if(Input.GetKey(KeyCode.Space) && jumpKeyPressed){
            isJumping = true;
        }


        if(Input.GetKeyUp(KeyCode.Space)){
            jumpKeyRealesed = true;
        }

        if(Input.GetAxisRaw("Horizontal") == 0){
            isStandingStill = true;
        }


        if(Input.GetAxisRaw("Horizontal") > 0){
            isStandingStill = false;
            if(Input.GetAxisRaw("Horizontal") < 0){
                isChangingDirection = true;
            }
        }

        if(Input.GetAxisRaw("Horizontal") < 0){
            isStandingStill= false;
            if(Input.GetAxisRaw("Horizontal") > 0){
                isChangingDirection = true;
            }
        }
    }

        

    

// Fixed update is called every physics update.
    private void FixedUpdate(){
        

         // check if player is touching the ground.
        if(Physics.OverlapSphere(groundCheckTransform.position,0.1f).Length == 1){
            return;
        }
        
        if(jumpKeyPressed){
        }

        if(isJumping){
            if(maxJump >= jumpForce){
                jumpForce += JumpIncreased * Time.deltaTime;
            } else {
                return;
            }
        }

        if(jumpKeyRealesed){
            jumpKeyPressed = false;
            rigidbodyComponent.AddForce(Vector3.up * jumpForce,ForceMode.VelocityChange);
            isJumping = false;
            jumpKeyRealesed = false;
            jumpForce = 3.0f;
        }
        

        if(maxSpeed >= walkingSpeed && !jumpKeyPressed){
            walkingSpeed += increasingSpeed * Time.deltaTime;
        }

        if(isStandingStill){
            walkingSpeed = 1.0f;
        }

        if(isLanding){
            walkingSpeed = walkingSpeed /2;
            isLanding = false;
        }

        if(isChangingDirection){
            walkingSpeed = 0.1f;
            isChangingDirection = false;
        }


        

        rigidbodyComponent.velocity = new Vector3(horizantalInput * walkingSpeed, rigidbodyComponent.velocity.y , 0);
    }



    }

