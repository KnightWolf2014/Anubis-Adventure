using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;



public class CynoController : MonoBehaviour {

    private struct MoveDirection {
        public float forward;
        public float lateral;
        public float up;
    }


    //Move variables
    private MoveDirection moveDirection;
    private float runSpeedForward;
    private float runSpeedLateral;

    //Jumping variables
    private float initJumpVelocity;
    private float maxJumpHeight;
    private float maxJumpTime;
    private bool isJumping;
    private bool isFalling;
    private bool canJump;

    //Dashing variables
    private bool isDashing;
    private bool canDash;
    private float timeDashing;

    //Gravity variables
    private float gravity;
    private float gravityOnGround;
    private float gravityFallMultiplier;

    //Menu variables
    public static bool menuBool;
    public static bool canMenu;
    
    //Components variables
    private CharacterController characterController;
    private Animator cynoAnim;

    //Variables para definir collider de character controller
    Vector3 centerCollider;
    Vector3 centerColliderDash;
    float height;
    float heightDashing;

    bool isAlive;

    //Variables para cambiar de direccion
    [SerializeField] private MapManager mapManager;
    bool canChangeDirection;
    bool firstChangeDir;
    GameObject changeDirectionGO;

    // Start is called before the first frame update
    void Start() {

        characterController = GetComponent<CharacterController>();
        cynoAnim = GetComponent<Animator>();

        centerCollider = characterController.center;
        centerColliderDash = new Vector3(characterController.center.x, characterController.center.y / 2.0f, characterController.center.z);

        height = characterController.height;
        heightDashing = characterController.height / 2.0f;

        moveDirection.forward = 0.0f;
        moveDirection.lateral = 0.0f;
        moveDirection.up = 0.0f;

        runSpeedForward = 30.0f;
        runSpeedLateral = 15.0f;

        FindFirstObjectByType<AudioManager>().playSound("running");

        
        menuBool = false;
        canMenu = true;

        configureJump();

        canDash = true;
        isAlive = true;

        canChangeDirection = false;
        firstChangeDir = true;

        changeDirectionGO = null;
    }


    // Update is called once per frame
    void Update() {

        if (isAlive) {

            handleRunDirection();

            calculateMovementRotation();

            handleGravity();
            handleDash();
            handleJump();

            handleMenu();
        }

    }

    private void calculateMovementRotation() {

        int rot = (int) this.transform.eulerAngles.y;
        Vector3 moveXYZ = new Vector3(0, moveDirection.up, 0);

        if (rot == 90) {
            moveXYZ.x = moveDirection.forward;
            moveXYZ.z = -moveDirection.lateral;


        } else if (rot == 180) {
            moveXYZ.z = -moveDirection.forward;
            moveXYZ.x = -moveDirection.lateral;


        } else if (rot == 270) {
            moveXYZ.x = -moveDirection.forward;
            moveXYZ.z = moveDirection.lateral;


        } else {
            moveXYZ.z = moveDirection.forward;
            moveXYZ.x = moveDirection.lateral;
        
        }

        characterController.Move(moveXYZ * Time.deltaTime);
    }

    private void handleMenu()
    {

        bool menuKey = Input.GetKeyDown(KeyCode.Escape);
        if (!menuBool && menuKey && canMenu)
        {
            PlayerManager.menu = true;
            menuBool = true;
            canMenu = false;
            FindFirstObjectByType<AudioManager>().playSound("Page");
            FindFirstObjectByType<AudioManager>().stopSound("Running");
            StartCoroutine(canMenuRoutine());
        }
        else if (menuBool && menuKey && canMenu)
        {
            PlayerManager.menu = false;
            menuBool = false;
            canMenu = false;
            FindFirstObjectByType<AudioManager>().playSound("Page");
            FindFirstObjectByType<AudioManager>().playSound("Running");
            StartCoroutine(canMenuRoutine());
        }
    }

    private void configureJump() {
        maxJumpHeight = 3.0f;
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
        moveDirection.forward = runSpeedForward;

        if (Input.GetKey(KeyCode.LeftArrow)) {
            if (!isJumping && !isDashing)
                moveDirection.lateral = -runSpeedLateral;
            else
                moveDirection.lateral = -runSpeedLateral * 0.85f;

        } else if (Input.GetKey(KeyCode.RightArrow)) {
            if (!isJumping && !isDashing)
                moveDirection.lateral = runSpeedLateral;
            else
                moveDirection.lateral = runSpeedLateral * 0.85f;

        } else {
            moveDirection.lateral = 0;
        }

        if (canChangeDirection && firstChangeDir && Input.GetKeyDown(KeyCode.A)) {
            this.transform.eulerAngles -= new Vector3(0, 90.0f, 0);
            firstChangeDir = false;
            Vector3 spawn = changeDirectionGO.transform.Find("leftSpawn").transform.position;
            mapManager.changeDirection(spawn);

        } else if (canChangeDirection && firstChangeDir && Input.GetKeyDown(KeyCode.S)) {
            this.transform.eulerAngles += new Vector3(0, +90.0f, 0);
            firstChangeDir = false;
            Vector3 spawn = changeDirectionGO.transform.Find("rightSpawn").transform.position;
            mapManager.changeDirection(spawn);
        }
    }

    private void handleDash() {
        if (characterController.isGrounded) {
            bool dashKey = Input.GetKeyDown(KeyCode.DownArrow);

            if (!isDashing && dashKey && canDash) {

                isDashing = true;
                canDash = false;

                characterController.height = heightDashing;
                characterController.center = centerColliderDash; 

                FindFirstObjectByType<AudioManager>().stopSound("Running");
                FindFirstObjectByType<AudioManager>().playSound("Slide");
                cynoAnim.CrossFade("Running Slide", 0.2f);
                StartCoroutine(canDashRoutine());
            }
        }
    }


    private void handleJump() {
        if (characterController.isGrounded) {
            bool jumpKeyUp = Input.GetKeyDown(KeyCode.UpArrow);

            if (!isJumping && jumpKeyUp &&  canJump) {

                if (isDashing) {
                    isDashing = false;
                    characterController.height = height;
                    characterController.center = centerCollider;
                    FindFirstObjectByType<AudioManager>().stopSound("Slide");
                }

                isJumping = true;
                moveDirection.up = initJumpVelocity;

                FindFirstObjectByType<AudioManager>().stopSound("Running");
                FindFirstObjectByType<AudioManager>().playSound("Jump");
                cynoAnim.CrossFade("Jumping Up",0.2f);

            } else if (isJumping && !jumpKeyUp) {
                isJumping = false;
            }
        } 
    }

    private void handleGravity() {

        if (characterController.isGrounded) {
            moveDirection.up = gravityOnGround;
            if (isFalling && !isDashing) {
                isFalling = false;
                canJump = false;
                StartCoroutine(canJumpRoutine());
                cynoAnim.CrossFade("Running", 0.4f);
            }

        } else {
            float prevYVel = moveDirection.up;

            if (!isFalling && prevYVel <= 0.0f) {
                isFalling = true;
                cynoAnim.CrossFade("Jumping Down", 0.1f);
            }
     
            float nextYVel;
            if (isFalling) {
                nextYVel = moveDirection.up + gravity * gravityFallMultiplier * Time.deltaTime;

            } else nextYVel = moveDirection.up + gravity * Time.deltaTime;

            moveDirection.up = (prevYVel+nextYVel)/2.0f;
        }
    }

    IEnumerator canJumpRoutine() {
        yield return new WaitForSeconds(0.05f);
        canJump = true;

        FindFirstObjectByType<AudioManager>().stopSound("Jump");
        FindFirstObjectByType<AudioManager>().playSound("Running");

    }

    IEnumerator canDashRoutine(){
        yield return new WaitForSeconds(1.25f);
        canDash = true;
        isDashing = false;
        characterController.height = height;
        characterController.center = centerCollider;

        cynoAnim.CrossFade("Running", 0.5f);
        FindFirstObjectByType<AudioManager>().stopSound("Slide");
        FindFirstObjectByType<AudioManager>().playSound("Running");
    }

    IEnumerator canMenuRoutine()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        canMenu = true;

    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "full_vida") {

            other.enabled = false;
            isAlive = false;

            cynoAnim.CrossFade("Knock", 0.1f);
            FindFirstObjectByType<AudioManager>().stopSound("Running");
            FindFirstObjectByType<AudioManager>().playSound("hit");

            PlayerManager.gameOver = true;

        } else if (other.tag == "media_vida") {
            other.enabled = false;
            Debug.Log("Half");

        } 
    }

    private void OnTriggerStay(Collider other) {

        if (!canChangeDirection && other.tag == "changeDirection") {
            canChangeDirection = true;
            changeDirectionGO = other.gameObject;
            Debug.Log(changeDirectionGO.name);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "changeDirection") {
            canChangeDirection = false;
            firstChangeDir = true;
        }
    }

}
