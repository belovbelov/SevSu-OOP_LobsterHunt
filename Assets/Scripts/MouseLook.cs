using UnityEngine;

namespace Assets.Scripts
{
    public class MouseLook : MonoBehaviour
    {
        #region Variables

        public float mouseSensetivity = 400f;
        public Transform playerBody;
        float xRotation;
        float mouseX;
        float mouseY;

        #endregion

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }


        private void Update()
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