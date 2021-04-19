using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class MouseLook : MonoBehaviour
    {
        #region Variables

        public float mouseSensetivity = 400f;
        public Transform playerBody;
        float xRotation = 0f;
        float mouseX;
        float mouseY;

        #endregion
        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }


        void Update()
        {
            mouseX = Input.GetAxis("Mouse X") * mouseSensetivity * Time.deltaTime;
            mouseY = Input.GetAxis("Mouse Y") * mouseSensetivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }
}