using UnityEngine;

namespace Assets.Scripts
{
    public class MouseLook : MonoBehaviour
    {
        #region Variables

        public float MouseSensetivity = 400f;
        public Transform PlayerBody;
        private float xRotation;
        private float mouseX;
        private float mouseY;

        #endregion

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }


        private void Update()
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