using UnityEngine;

public class Player : Creature
{
    Player() : base()
    {
        playerSpeed = creatureSpeed;
    }

    #region Variables

    // Movement variables
    public CharacterController controller;
    float playerSpeed;
    public float gravity = -19.62f;
    public Transform groundCheck;
    public float groundDistance = 0.21f;
    public LayerMask groundMask;
    public float jumpHeight = 1f;
    public float sprintModifier;
    Vector3 velocity;
    bool isSprinting;
    public Transform waterCheck;
    bool sprint;
    bool jump;
    bool crouch;
    float timeInWater = 0.0f;

    // States
    bool isGrounded;
    bool isJumping;
    bool isSwimming;
    bool isArising;
    bool isCrouching;
    float adjustedSpeed;

    //FOV
    public Transform weaponParent;
    public Camera normalCam;
    float baseFOV;
    float sprintFOVModifier = 1.25f;
    Vector3 weaponOrigin;

    //weaponBob
    private Vector3 weaponParentCurrentPos;
    private Vector3 targetWeaponBobPosition;
    private float movementCounter;
    private float idleCounter;


    #endregion

    void Start()
    {
        baseFOV = normalCam.fieldOfView;
        weaponOrigin = weaponParent.transform.localPosition;
        weaponParentCurrentPos = weaponOrigin;
    }

    void Update()
    {
        Move();
    }

    public override void Move()
    {
        //Axles
        velocity.x = Input.GetAxis("Horizontal");
        velocity.z = Input.GetAxis("Vertical");


        //Controls объявление переменных за update
        sprint = Input.GetKey(KeyCode.LeftControl);
        jump = Input.GetButton("Jump");
        crouch = Input.GetKey(KeyCode.LeftShift);


        //States
        isSprinting = sprint && velocity.z > 0;
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        isJumping = jump && isGrounded;

        isSwimming = waterCheck.position.y > transform.position.y;
        isArising = jump && isSwimming;
        isCrouching = crouch && isSwimming && !isArising;



        //Movement
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        adjustedSpeed = playerSpeed;
        if (isSprinting)
        {
            adjustedSpeed *= sprintModifier;
        }
        Vector3 move = (transform.right * velocity.x + transform.forward * velocity.z);
        if (move.magnitude > 1)
        {
            move.Normalize();
        }
        controller.Move(move * adjustedSpeed * Time.deltaTime);

        if (isJumping)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        if (isSwimming)
        {
            timeInWater += Time.deltaTime;
            velocity.y = Mathf.Lerp(velocity.y, -2.0f, Time.deltaTime * 6.0f);
            if (isArising && timeInWater > 0.15f)
            {
                velocity.y = Mathf.Lerp(velocity.y, Mathf.Sqrt(-gravity * 2.0f), Time.deltaTime * 8.0f);
            }
            if (isCrouching)
            {
                velocity.y = Mathf.Lerp(velocity.y, -Mathf.Sqrt(-gravity * 2.0f), Time.deltaTime * 8.0f);
            }
        }
        else
        {
            timeInWater = 0.0f;
            velocity.y += gravity * Time.deltaTime;
        }
        controller.Move(velocity * Time.deltaTime);


        //FOV
        if (isSprinting)
        {
            normalCam.fieldOfView = Mathf.Lerp(normalCam.fieldOfView, baseFOV * sprintFOVModifier, Time.deltaTime * 8f);
        }
        else
        {
            normalCam.fieldOfView = Mathf.Lerp(normalCam.fieldOfView, baseFOV, Time.deltaTime * 8f);
        }
        //Head Bob
        if (!isGrounded && !isSwimming)
        {
            //airborne
            HeadBob(idleCounter, 0.015f, 0.015f);
            idleCounter += 0;
            weaponParent.localPosition = Vector3.MoveTowards(weaponParent.localPosition, targetWeaponBobPosition, Time.deltaTime * 2f * 0.2f);
        }
        else if (velocity.x == 0 && velocity.z == 0)
        {
            //idling
            HeadBob(idleCounter, 0.015f, 0.015f);
            idleCounter += Time.deltaTime;
            weaponParent.localPosition = Vector3.MoveTowards(weaponParent.localPosition, targetWeaponBobPosition, Time.deltaTime * 1f * 0.2f);
        }
        else if (!isSprinting && (velocity.x != 0 || velocity.z != 0))
        {
            //walking
            HeadBob(movementCounter, 0.015f, 0.015f);
            movementCounter += Time.deltaTime * 5f;
            weaponParent.localPosition = Vector3.MoveTowards(weaponParent.localPosition, targetWeaponBobPosition, Time.deltaTime * 12f * 0.2f);
        }
        else
        {
            //sprinting
            HeadBob(movementCounter, 0.025f, 0.025f);
            movementCounter += Time.deltaTime * 6.75f;
            weaponParent.localPosition = Vector3.MoveTowards(weaponParent.localPosition, targetWeaponBobPosition, Time.deltaTime * 12f * 0.25f);
        }
    }

    void HeadBob(float p_z, float p_x_intensity, float p_y_intensity)
    {
        float t_aim_adjust = 1f;
        targetWeaponBobPosition = weaponParentCurrentPos + new Vector3(Mathf.Cos(p_z) * p_x_intensity * t_aim_adjust, Mathf.Sin(p_z * 2) * p_y_intensity * t_aim_adjust, 0);
    }
}
