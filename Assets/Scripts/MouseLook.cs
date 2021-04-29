using UnityEngine;

namespace Lobster
{
    public class MouseLook : MonoBehaviour
    {
        #region Variables

        private float MouseSensetivity { get; set; } = 200f;
        public Transform PlayerBody;
        private float xRotation;
        private float mouseX;
        private float mouseY;

        #endregion

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }


        private void FixedUpdate()
        {
            mouseX = Input.GetAxis("Mouse X") * MouseSensetivity * Time.deltaTime;
            mouseY = Input.GetAxis("Mouse Y") * MouseSensetivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            PlayerBody.Rotate(Vector3.up * mouseX);
        }
    }
}