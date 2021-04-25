using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts.Entities
{
    public class Player : Creature
    {

        #region Variables

        // Movement variables
        public CharacterController Controller;
        private readonly float playerSpeed;
        [SerializeField] private readonly float gravity = -19.62f;
        public Transform GroundCheck;
        [SerializeField] private readonly float groundDistance = 0.21f;
        public LayerMask GroundMask;
        [SerializeField] private readonly float jumpHeight = 1f;
        [SerializeField] private readonly float sprintModifier = 2f;

        private Vector3 velocity;
        public Transform WaterCheck;
        private bool sprint;
        private bool jump;
        private bool crouch;
        private float timeUnderWater;
        private float timeToFloat;

        public OxygenUI Slider;

        // States
        private bool isSprinting;
        private bool isGrounded;
        private bool isJumping;
        private bool isSwimming;
        private bool isArising;
        private bool isCrouching;
        private float adjustedSpeed;
        public bool IsBreathing;
        public bool IsDead;

        private bool canBreathe;

        //FOV
        public Transform WeaponParent;
        public Camera NormalCam;

        private float baseFov;
        private const float SprintFovModifier = 1.25f;
        private Vector3 weaponOrigin;

        //weaponBob
        public Vector3 WeaponParentCurrentPos
        {
            get => weaponParentCurrentPos;
            set => weaponParentCurrentPos = value;
        }

        private Vector3 weaponParentCurrentPos;

        public Vector3 TargetWeaponBobPosition
        {
            get => targetWeaponBobPosition;
            set => targetWeaponBobPosition = value;
        }

        private Vector3 targetWeaponBobPosition;

        public float MovementCounter
        {
            get => movementCounter;
            set => movementCounter = value;
        }

        private float movementCounter;

        public float IdleCounter
        {
            get => idleCounter;
            set => idleCounter = value;
        }

        private float idleCounter;


        #endregion

        private Player() => playerSpeed = CreatureSpeed;

        private void Start()
        {
            baseFov = NormalCam.fieldOfView;
            weaponOrigin = WeaponParent.transform.localPosition;
            weaponParentCurrentPos = weaponOrigin;

            IsBreathing = true;
            IsDead = false;
        }

        private void Update()
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
            isGrounded = Physics.CheckSphere(GroundCheck.position, groundDistance, GroundMask);
            isJumping = jump && isGrounded;

            isSwimming = WaterCheck.position.y > transform.position.y - 0.3f;
            canBreathe = WaterCheck.position.y < NormalCam.transform.position.y;
            isArising = jump && isSwimming;
            isCrouching = crouch && isSwimming && !isArising;

            IsBreathing = (1 - timeUnderWater / 30.0f) > 0;
            Slider.SetSlider(1 - timeUnderWater / 30.0f);

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

            Vector3 move = transform.right * velocity.x + transform.forward * velocity.z;
            if (move.magnitude > 1)
            {
                move.Normalize();
            }

            Controller.Move(move * adjustedSpeed * Time.deltaTime);

            if (isJumping)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            if (!canBreathe)
            {
                timeUnderWater += Time.deltaTime;
            }

            if (isSwimming)
            {
                timeToFloat += Time.deltaTime;
                velocity.y = Mathf.Lerp(velocity.y, -2.0f, Time.deltaTime * 6.0f);
                if (isArising && timeToFloat > 0.1f)
                {
                    velocity.y = Mathf.Lerp(velocity.y, Mathf.Sqrt(-gravity * 3.0f), Time.deltaTime * 8.0f);
                }

                if (isCrouching)
                {
                    velocity.y = Mathf.Lerp(velocity.y, -Mathf.Sqrt(-gravity * 4.0f), Time.deltaTime * 8.0f);
                }
            }
            else
            {
                timeToFloat = 0f;
            }

            if (canBreathe)
            {
                timeUnderWater = Mathf.Lerp(timeUnderWater, 0.0f, Time.deltaTime * 0.5f);
                velocity.y += gravity * Time.deltaTime;
            }

            Controller.Move(velocity * Time.deltaTime);


            //FOV
            ChangeFov();

            HeadBob bob = CheckState();

            bob.doHeadBob();
            //Head Bob
            /*if (!isGrounded && !isSwimming)
            {
                //airborne
                HeadBob(idleCounter, 0.015f, 0.015f);
                idleCounter += Time.deltaTime * 0.5f;
                WeaponParent.localPosition = Vector3.MoveTowards(WeaponParent.localPosition, targetWeaponBobPosition, Time.deltaTime * 2f * 0.2f);
            }
            else if (velocity.x == 0 && velocity.z == 0 || isSwimming)
            {
                //idling
                HeadBob(idleCounter, 0.015f, 0.015f);
                idleCounter += Time.deltaTime;
                WeaponParent.localPosition = Vector3.MoveTowards(WeaponParent.localPosition, targetWeaponBobPosition, Time.deltaTime * 2f * 0.1f);
            }
            else if (!isSwimming && !isSprinting && (velocity.x != 0 || velocity.z != 0))
            { //walking
                HeadBob(movementCounter, 0.015f, 0.015f);
                movementCounter += Time.deltaTime * 5f;
                WeaponParent.localPosition = Vector3.MoveTowards(WeaponParent.localPosition, targetWeaponBobPosition, Time.deltaTime * 12f * 0.15f);
            }
            else
            { //sprinting
                HeadBob(movementCounter, 0.02f, 0.02f);
                movementCounter += Time.deltaTime * 6.75f;
                WeaponParent.localPosition = Vector3.MoveTowards(WeaponParent.localPosition, targetWeaponBobPosition, Time.deltaTime * 12f * 0.25f);
            }*/
        }

        // private void HeadBob(float pZ, float pXIntensity, float pYIntensity)
        // {
        //     float tAimAdjust = 1f;
        //     targetWeaponBobPosition = weaponParentCurrentPos + new Vector3(Mathf.Cos(pZ) * pXIntensity * tAimAdjust,
        //         Mathf.Sin(pZ * 2) * pYIntensity * tAimAdjust, 0);
        // }
        private void ChangeFov()
        {
            if (isSprinting)
            {
                targetWeaponBobPosition.z -= 0.2f;
                NormalCam.fieldOfView =
                    Mathf.Lerp(NormalCam.fieldOfView, baseFov * SprintFovModifier, Time.deltaTime * 8f);
            }
            else
            {
                NormalCam.fieldOfView = Mathf.Lerp(NormalCam.fieldOfView, baseFov, Time.deltaTime * 8f);
            }

        }

        private HeadBob CheckState()
        {
            if (!isGrounded && !isSwimming)
            {
                return new HeadBobJump(this);
            }

            if (velocity.x == 0 && velocity.z == 0 || isSwimming)
            {
                return new HeadBobIdle(this);
            }

            if (!isSwimming && !isSprinting && (velocity.x != 0 || velocity.z != 0))
            {
                return new HeadBobWalk(this);
            }

            return new HeadBobRun(this);
        }
    }
}
