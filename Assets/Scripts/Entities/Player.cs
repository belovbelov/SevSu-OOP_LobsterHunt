using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts.Entities
{
    public class Player : Creature
    {
        private Player() => playerSpeed = CreatureSpeed;

        #region Variables
        // Movement variables
        public CharacterController Controller;
        private float playerSpeed;
        [SerializeField]
        private float Gravity = -19.62f;
        public Transform GroundCheck;
        [SerializeField]
        private float GroundDistance = 0.21f;
        public LayerMask GroundMask;
        [SerializeField]
        private float JumpHeight = 1f;
        public float SprintModifier;
        private Vector3 Velocity;
        private bool isSprinting;
        public Transform WaterCheck;
        private bool sprint;
        private bool jump;
        private bool crouch;
        private float timeUnderWater;
        private float timeToFloat;

        public OxygenUI Slider;

        // States
        private bool isGrounded;
        private bool isJumping;
        private bool isSwimming;
        private bool isArising;
        private bool isCrouching;
        private float adjustedSpeed;
        public static bool IsBreathing;
        public static bool IsDead;

        private bool canBreathe;
        //FOV
        public Transform WeaponParent;
        public Camera NormalCam;
        private float baseFov;
        private const float SprintFovModifier = 1.25f;
        private Vector3 weaponOrigin;

        //weaponBob
        private Vector3 weaponParentCurrentPos;
        private Vector3 targetWeaponBobPosition;
        private float movementCounter;
        private float idleCounter;


        #endregion

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
            Velocity.x = Input.GetAxis("Horizontal");
            Velocity.z = Input.GetAxis("Vertical");


            //Controls объявление переменных за update
            sprint = Input.GetKey(KeyCode.LeftControl);
            jump = Input.GetButton("Jump");
            crouch = Input.GetKey(KeyCode.LeftShift);


            //States
            isSprinting = sprint && Velocity.z > 0;
            isGrounded = Physics.CheckSphere(GroundCheck.position, GroundDistance, GroundMask);
            isJumping = jump && isGrounded;

            isSwimming = WaterCheck.position.y > transform.position.y - 0.3f;
            canBreathe = WaterCheck.position.y < NormalCam.transform.position.y;
            isArising = jump && isSwimming;
            isCrouching = crouch && isSwimming && !isArising;

            IsBreathing = (1 - timeUnderWater / 30.0f) > 0;
            Slider.SetSlider(1 - timeUnderWater / 30.0f);

            //Movement
            if (isGrounded && Velocity.y < 0)
            {
                Velocity.y = -2f;
            }

            adjustedSpeed = playerSpeed;
            if (isSprinting)
            {
                adjustedSpeed *= SprintModifier;
            }
            Vector3 move = transform.right * Velocity.x + transform.forward * Velocity.z;
            if (move.magnitude > 1)
            {
                move.Normalize();
            }
            Controller.Move(move * adjustedSpeed * Time.deltaTime);

            if (isJumping)
            {
                Velocity.y = Mathf.Sqrt(JumpHeight * -2f * Gravity);
            }
            if (!canBreathe)
            {
                timeUnderWater += Time.deltaTime;
            }

            if (isSwimming)
            {
                timeToFloat += Time.deltaTime;
                Velocity.y = Mathf.Lerp(Velocity.y, -2.0f, Time.deltaTime * 6.0f);
                if (isArising && timeToFloat > 0.1f)
                {
                    Velocity.y = Mathf.Lerp(Velocity.y, Mathf.Sqrt(-Gravity * 3.0f), Time.deltaTime * 8.0f);
                }
                if (isCrouching)
                {
                    Velocity.y = Mathf.Lerp(Velocity.y, -Mathf.Sqrt(-Gravity * 2.0f), Time.deltaTime * 8.0f);
                }
            }
            else
            {
                timeToFloat = 0f;
            }
            if (canBreathe)
            {
                timeUnderWater = Mathf.Lerp(timeUnderWater, 0.0f, Time.deltaTime * 0.5f);
                Velocity.y += Gravity * Time.deltaTime;
            }
            Controller.Move(Velocity * Time.deltaTime);


            //FOV
            if (isSprinting)
            {
                targetWeaponBobPosition.z -= 0.2f;
                NormalCam.fieldOfView = Mathf.Lerp(NormalCam.fieldOfView, baseFov * SprintFovModifier, Time.deltaTime * 8f);
            }
            else
            {
                NormalCam.fieldOfView = Mathf.Lerp(NormalCam.fieldOfView, baseFov, Time.deltaTime * 8f);
            }

            //Head Bob
            if (!isGrounded && !isSwimming)
            {
                //airborne
                HeadBob(idleCounter, 0.015f, 0.015f);
                idleCounter += Time.deltaTime * 0.5f;
                WeaponParent.localPosition = Vector3.MoveTowards(WeaponParent.localPosition, targetWeaponBobPosition, Time.deltaTime * 2f * 0.2f);
            }
            else if (Velocity.x == 0 && Velocity.z == 0 || isSwimming)
            {
                //idling
                HeadBob(idleCounter, 0.015f, 0.015f);
                idleCounter += Time.deltaTime;
                WeaponParent.localPosition = Vector3.MoveTowards(WeaponParent.localPosition, targetWeaponBobPosition, Time.deltaTime * 2f * 0.1f);
            }
            else if (!isSwimming && !isSprinting && (Velocity.x != 0 || Velocity.z != 0))
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
            }
        }

        private void HeadBob(float pZ, float pXIntensity, float pYIntensity)
        {
            float tAimAdjust = 1f;
            targetWeaponBobPosition = weaponParentCurrentPos + new Vector3(Mathf.Cos(pZ) * pXIntensity * tAimAdjust, Mathf.Sin(pZ * 2) * pYIntensity * tAimAdjust, 0);
        }

    }
}