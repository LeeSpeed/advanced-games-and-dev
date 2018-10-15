using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [SerializeField] float walkSpeed = 2.0f;
    [SerializeField] float runSpeed = 6.0f;
    [SerializeField] float gravity = -12.0f;
    [SerializeField] float rotationSmoothTime = 0.2f;
    [SerializeField] float speedSmoothTime = 0.05f;

    private float playerSpeed, animSpeedPercent, turnSmoothVelocity, speedSmoothVelocity, currentSpeed, velocityY;

    Animator animator;
    bool playerHasBeenSpotted;
    CharacterController characterController;
    Transform cameraTransform;

    void Start()
    {
        animator = GetComponent<Animator>();
        cameraTransform = Camera.main.transform;
        characterController = GetComponent<CharacterController>();
        PatrolBots.OnPlayerHasBeenSpotted += PlayerSpotted;
    }

    void Update()
    {
        Vector2 playerInput = Vector2.zero;
        
        if(!playerHasBeenSpotted)
        {
            playerInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }
        Vector2 inputDirection = playerInput.normalized;
        bool running = Input.GetKey(KeyCode.LeftShift);

        PlayerMovementAndRotation(inputDirection, running);

        // animations
        animSpeedPercent = ((running) ? currentSpeed / runSpeed : currentSpeed / walkSpeed * 0.5f);
        animator.SetFloat("speedPercent", animSpeedPercent, speedSmoothTime, Time.deltaTime);
    }

    void PlayerMovementAndRotation(Vector2 inputDirection, bool running)
    {
        if (inputDirection != Vector2.zero)
        {
            float targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.y) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, rotationSmoothTime);
        }
        
        playerSpeed = ((running) ? runSpeed : walkSpeed) * inputDirection.magnitude;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, playerSpeed, ref speedSmoothVelocity, speedSmoothTime);

        velocityY += Time.deltaTime * gravity;
        Vector3 velocity = transform.forward * currentSpeed + Vector3.up * velocityY;

        characterController.Move(velocity * Time.deltaTime);
        currentSpeed = new Vector2(characterController.velocity.x, characterController.velocity.z).magnitude;

        if(characterController.isGrounded)
        {
            velocityY = 0;
        }
    }

    void PlayerSpotted()
    {
        playerHasBeenSpotted = true;
    }

    void OnDestroy()
    {
        PatrolBots.OnPlayerHasBeenSpotted -= PlayerSpotted;
    }
}

//[SerializeField] float InputX, InputZ, playerRotationSpeed, speed, allowPlayerRotation, verticalVel;
//[SerializeField] bool isGrounded, blockRotationPlayer;
//public Vector3 desiredMoveDirection, moveVector;
//public Camera mainCamera;
//public CharacterController characterController;
//public Animator anim;

//// Use this for initialization
//void Start()
//{
//    mainCamera = Camera.main;
//    anim = GetComponent<Animator>();
//    characterController = GetComponent<CharacterController>();
//}

//// Update is called once per frame
//void Update()
//{
//    PlayerMagnitude();

//    isGrounded = characterController.isGrounded;
//    if (isGrounded)
//    {
//        verticalVel -= 0;
//    }
//    else
//    {
//        verticalVel -= 2;
//    }

//    moveVector = new Vector3(0, verticalVel, 0);
//    characterController.Move(moveVector);
//}

//public void PlayerMovementAndRotation()
//{
//    InputX = Input.GetAxis("Horizontal");
//    InputZ = Input.GetAxis("Vertical");

//    Vector3 forward = mainCamera.transform.forward;
//    Vector3 right = mainCamera.transform.right;

//    forward.y = 0f;
//    right.y = 0f;

//    forward.Normalize();
//    right.Normalize();

//    desiredMoveDirection = forward * InputZ + right * InputX;

//    if (blockRotationPlayer == false)
//    {
//        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), playerRotationSpeed);
//    }
//}


//void PlayerMagnitude()
//{
//    InputX = Input.GetAxis("Horizontal");
//    InputZ = Input.GetAxis("Vertical");

//    anim.SetFloat("InputX", InputX, 0.0f, Time.deltaTime * 2f);
//    anim.SetFloat("InputZ", InputZ, 0.0f, Time.deltaTime * 2f);

//    //Calculate input magnitude
//    speed = new Vector2(InputX, InputZ).sqrMagnitude;

//    //Move player
//    if (speed > allowPlayerRotation)
//    {
//        anim.SetFloat("Magnitude", speed, 0.0f, Time.deltaTime);
//        PlayerMovementAndRotation();
//    }
//    else if (speed < allowPlayerRotation)
//    {
//        anim.SetFloat("Magnitude", speed, 0.0f, Time.deltaTime);
//    }


//}


//[SerializeField] float inputDelay = 0.1f;
//[SerializeField] float forwardSpeed = 10.0f;
//[SerializeField] float rotationSpeed = 80.0f;

//Quaternion targetRotation;
//Rigidbody rBody;
//private float verticalInput, horizontalInput;

//public Quaternion TargetRotation
//{
//    get { return targetRotation; }
//}

//void Start()
//{
//    targetRotation = transform.rotation;
//    if(GetComponent<Rigidbody>())
//    {
//        rBody = GetComponent<Rigidbody>();
//    }
//    else
//    {
//        Debug.LogError("The character requires a rigidbody");
//    }

//    verticalInput = horizontalInput = 0;
//}

//void GetInput()
//{
//    verticalInput = Input.GetAxis("Vertical");
//    horizontalInput = Input.GetAxis("Horizontal");
//}

//void Update()
//{
//    GetInput();
//    PlayerRotate();
//}

//void FixedUpdate()
//{
//    PlayerRun();
//}

//void PlayerRun()
//{
//    if (Mathf.Abs(verticalInput) > inputDelay)
//    {
//        rBody.velocity = transform.forward * verticalInput * forwardSpeed;
//    }
//    else
//    {
//        rBody.velocity = Vector3.zero;
//    }

//}

//void PlayerRotate()
//{
//    if (Mathf.Abs(horizontalInput) > inputDelay)
//    {
//        targetRotation *= Quaternion.AngleAxis(rotationSpeed * horizontalInput * Time.deltaTime, Vector3.up);
//    }

//    transform.rotation = targetRotation;
//}


