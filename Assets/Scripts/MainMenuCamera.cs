using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    public class MainMenuCamera : MonoBehaviour
    {
        private float speedMod = 0.75f;

        void Update()
        {
            transform.RotateAround(transform.position, new Vector3(0.0f, 1.0f, 0.0f), 5 * Time.deltaTime * speedMod);
        }
    }
}