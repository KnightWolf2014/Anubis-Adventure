using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class CynoController : MonoBehaviour {

    //Move variables
    private Vector3 moveDirection;
    private float runSpeedZ;
    private float runSpeedX;


    //Jumping variables
    private float initJumpVelocity;
    private float maxJumpHeight;
    private float maxJumpTime;
    private bool isJumping;
    private bool isFalling;
    private bool canJump;

    //Gravity variables
    private float gravity;
    private float gravityOnGround;
    private float gravityFallMultiplier;

    //Components variables
    private CharacterController characterController;
    private Animator cynoAnim;

    // Start is called before the first frame update
    void Start() {

        characterController = GetComponent<CharacterController>();
        cynoAnim = GetComponent<Animator>();

        moveDirection = Vector3.zero;
        runSpeedZ = 20.0f;
        runSpeedX = 10.0f;

        configureJump();
    }


    // Update is called once per frame
    void Update() {

        handleRunDirection();
        characterController.Move(moveDirection * Time.deltaTime);

        handleGravity();
        handleJump();

    }

    private void configureJump() {
        maxJumpHeight = 5.0f;
        maxJumpTime = 0.75f;
        isJumping = false;
        isFalling = false;
        canJump = true;

        float halfJumpTime = maxJumpTime/2.0f;
        initJumpVelocity = (2.0f * maxJumpHeight) / halfJumpTime;

        gravity = (-2 * maxJumpHeight) / Mathf.Pow(halfJumpTime, 2);
        gravityOnGround = -0.05f;
        gravityFallMultiplier = 2.0f;
    }

    private void handleRunDirection() {
        moveDirection.z = runSpeedZ;

        if (Input.GetKey(KeyCode.A)) {
            moveDirection.x = -runSpeedX;

        } else if (Input.GetKey(KeyCode.D)) {
            moveDirection.x = runSpeedX;

        } else { 
            moveDirection.x = 0;
        
        }

        //TODO GIR:
    }

    private void handleJump() {
        if (characterController.isGrounded) {
            bool jumpKeyUp = Input.GetKeyDown(KeyCode.Space);

            if (!isJumping && jumpKeyUp &&  canJump) {
                isJumping = true;
                moveDirection.y = initJumpVelocity;
                cynoAnim.CrossFade("Jumping Up",0.2f);

            } else if (isJumping && !jumpKeyUp) {
                isJumping = false;
            }
        } 
    }

    private void handleGravity() {

        if (characterController.isGrounded) {
            moveDirection.y = gravityOnGround;
            if (isFalling) {
                isFalling = false;
                canJump = false;
                StartCoroutine(canJumpRoutine());
                cynoAnim.CrossFade("Running", 0.5f);
            }

        } else {
            float prevYVel = moveDirection.y;

            if (!isFalling && prevYVel <= 0.0f) {
                isFalling = true;
                cynoAnim.CrossFade("Jumping Down", 0.2f);
            }
     
            float nextYVel;
            if (isFalling) {
                nextYVel = moveDirection.y + gravity * gravityFallMultiplier * Time.deltaTime;

            } else nextYVel = moveDirection.y + gravity * Time.deltaTime;

            moveDirection.y = (prevYVel+nextYVel)/2.0f;
        }
    }

    IEnumerator canJumpRoutine() {
        yield return new WaitForSeconds(0.3f);

        canJump = true;
    
    }
  
}
