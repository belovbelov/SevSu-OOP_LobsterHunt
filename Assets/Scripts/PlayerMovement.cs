using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    #region Variables

    // Movement variables
    public CharacterController controller;
    public float speed = 12f;
    public float gravity = -9.81f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public float jumpHeight = 3f;
    public Vector3 velocity;
    public float sprintModifier;
    bool isSprinting;

    //FOV
    public Transform weaponParent;
    public Camera normalCam;
    float baseFOV;
    float sprintFOVModifier = 1.25f;
    Vector3 weaponOrigin;

    //weaponBob
    float timer = 0f;
    #endregion
    void Start() {
        baseFOV = normalCam.fieldOfView;
        weaponOrigin = weaponParent.transform.localPosition;
    }
    void Update() {
        //Axles
        velocity.x = Input.GetAxis("Horizontal");
        velocity.z = Input.GetAxis("Vertical");


        //Controls
        bool sprint = Input.GetKey(KeyCode.LeftShift);
        bool jump = Input.GetButtonDown("Jump");


        //States
        isSprinting = sprint && velocity.z > 0;
        bool isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        bool isJumping = jump && isGrounded;


        //Movement
        if (isGrounded && velocity.y < 0) {
            velocity.y = -2f;
        }

        float adjustedSpeed = speed;
        if (isSprinting) {
            adjustedSpeed *= sprintModifier;
        }
        Vector3 move = (transform.right * velocity.x + transform.forward * velocity.z);
        if (move.magnitude > 1) {
            move.Normalize();
        }
        controller.Move(move * adjustedSpeed * Time.deltaTime);

        if (isJumping) {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        

        //FOV
        if (isSprinting) {
            normalCam.fieldOfView = Mathf.Lerp(normalCam.fieldOfView, baseFOV * sprintFOVModifier, Time.deltaTime * 8f);
        } else {
            normalCam.fieldOfView = Mathf.Lerp(normalCam.fieldOfView, baseFOV, Time.deltaTime * 8f);
        }
        weaponBob();
    }
    #region Private methods
    void weaponBob() {
        if (isSprinting && (Mathf.Abs(velocity.x) > 0.35f || Mathf.Abs(velocity.z) > 0.35f)) {
            timer += Time.deltaTime;
            weaponParent.transform.localPosition = new Vector3(weaponOrigin.x, weaponOrigin.y, Mathf.Sin(timer * 8f) * 0.1f);
        } else {
            if (Mathf.Abs(velocity.x) > 0.35f || Mathf.Abs(velocity.z) > 0.35f) {
                timer += Time.deltaTime;
                weaponParent.transform.localPosition = new Vector3(weaponOrigin.x, weaponOrigin.y, Mathf.Sin(timer * 4f) * 0.1f);
            } else {
                timer = 0;
                timer += Time.deltaTime;
                weaponParent.transform.localPosition = new Vector3(weaponOrigin.x, weaponOrigin.y, Mathf.Lerp(weaponParent.transform.localPosition.z, 0, timer * 5f));
            }
        }
    }
    #endregion
}
