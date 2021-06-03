using System;
using Lobster.UI;
using UnityEngine;

namespace Lobster.Entities
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
        [SerializeField] public float SwimModifier = 0.75f;
        [SerializeField] public float OxygenReduceRate = 30.0f;

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
        private float oxygenRecoveryRate=0.05f;
        [SerializeField]private float adjustedSpeed;
        public bool IsBreathing { get; set; }
        public bool IsDead { get; set; }

        private bool canBreathe;

        //FOV
        public Transform WeaponParent;
        public Camera NormalCam;

        private float baseFov;
        private const float SprintFovModifier = 1.25f;
        private Vector3 weaponOrigin;

        //weaponBob
        public Vector3 WeaponParentCurrentPos { get; set; }

        public Vector3 TargetWeaponBobPosition { get; set; }

        public float MovementCounter { get; set; }

        public float IdleCounter { get; set; }
        #endregion

        private Player() => playerSpeed = CreatureSpeed;

        private void Start()
        {
            baseFov = NormalCam.fieldOfView;
            weaponOrigin = WeaponParent.transform.localPosition;
            WeaponParentCurrentPos = weaponOrigin;

            IsBreathing = true;
            IsDead = false;
            SwimModifier *= Score.Instance.Speed;
            OxygenReduceRate *= Score.Instance.Oxygen;
        }

        private void Update()
        {
            Move();
        }

        private void FixedUpdate()
        {
            velocity.x = Input.GetAxis("Horizontal");
            velocity.z = Input.GetAxis("Vertical");


            //Controls объявление переменных за update
            sprint = Input.GetKey(KeyCode.LeftControl);
            jump = Input.GetButton("Jump");
            crouch = Input.GetKey(KeyCode.LeftShift);

        }

        public override void Move()
        {
            //States
            isSprinting = sprint && velocity.z > 0;
            isGrounded = Physics.CheckSphere(GroundCheck.position, groundDistance, GroundMask);
            isJumping = jump && isGrounded;
            isSwimming = WaterCheck.position.y > GroundCheck.transform.position.y;
            canBreathe = WaterCheck.position.y < NormalCam.transform.position.y;
            isArising = jump && isSwimming;
            isCrouching = crouch && isSwimming && !isArising;

            IsBreathing = (1 - timeUnderWater / OxygenReduceRate) > 0;
            Slider.SetSlider(1 - timeUnderWater / OxygenReduceRate);

            //Movement
            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            adjustedSpeed = playerSpeed;
            if (isSwimming)
            {
                adjustedSpeed *= SwimModifier;
            }
            if (isSprinting)
            {
                adjustedSpeed *= sprintModifier;
            }

            var move = transform.right * velocity.x + transform.forward * velocity.z;
            if (move.magnitude > 1)
            {
                move.Normalize();
            }

            Controller.Move(move * (adjustedSpeed * Time.deltaTime));

            if (isJumping)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            if (!canBreathe)
            {
                timeUnderWater += Time.deltaTime;
            }
            else
            {
                timeUnderWater = timeUnderWater > 0 ? timeUnderWater -= oxygenRecoveryRate : timeUnderWater = 0;
                velocity.y += gravity * Time.deltaTime;
            }

            if (isSwimming)
            {
                timeToFloat += Time.deltaTime;
                velocity.y = Mathf.Lerp(velocity.y, -2.0f, Time.deltaTime * 6.0f);
                if (isArising && timeToFloat > 0.1f)
                {
                    velocity.y = Mathf.Lerp(velocity.y, Mathf.Sqrt(-gravity * 3.0f)*SwimModifier, Time.deltaTime * 8.0f);
                }

                if (isCrouching)
                {
                    velocity.y = Mathf.Lerp(velocity.y, -Mathf.Sqrt(-gravity * 4.0f)*SwimModifier, Time.deltaTime * 8.0f);
                }
            }
            else
            {
                timeToFloat = 0f;
            }

            Controller.Move(velocity * Time.deltaTime);


            //FOV
            ChangeFov();

            var bob = CheckState();

            bob.DoHeadBob();
        }

        private void ChangeFov()
        {
            if (isSprinting)
            {
                NormalCam.fieldOfView = Mathf.Lerp(NormalCam.fieldOfView, baseFov * SprintFovModifier, Time.deltaTime * 8f);
                return;
            } 
            NormalCam.fieldOfView = Mathf.Lerp(NormalCam.fieldOfView, baseFov, Time.deltaTime * 8f);
        }

        private HeadBob CheckState()
        {//strategy pattern
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
