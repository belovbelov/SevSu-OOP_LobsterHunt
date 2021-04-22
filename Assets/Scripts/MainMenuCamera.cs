using UnityEngine;

namespace Assets.Scripts
{
    public class MainMenuCamera : MonoBehaviour
    {
        private const float SpeedMod = 0.75f;

        private void Update()
        {
            transform.RotateAround(transform.position, new Vector3(0.0f, 1.0f, 0.0f), 5 * Time.deltaTime * SpeedMod);
        }
    }
}