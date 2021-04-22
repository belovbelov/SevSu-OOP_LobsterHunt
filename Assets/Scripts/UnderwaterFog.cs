using UnityEngine;

namespace Assets.Scripts
{
    public class UnderwaterFog : MonoBehaviour
    {
        public Transform WaterPlane;

        private bool defaultFog;
        private float defaultFogDensity;

        public float Density = 0.075f;

        // Use this for initialization
        private void Start()
        {
            defaultFog = RenderSettings.fog;
            defaultFogDensity = RenderSettings.fogDensity;
        }

        // Update is called once per frame
        private void Update()
        {
            if (WaterPlane == null) return;

            // test the y position to see if it is under the plane
            SetFog(transform.position.y < WaterPlane.position.y + 0.425f);

            // test fog according to the bounding box of the water "box")
            // SetFog(waterPlane.gameObject.GetComponent<Renderer>().bounds.Contains(transform.position));

            // using a plane that can be rotated
            SetFog(WaterPlane.InverseTransformPoint(transform.position).y < 0.425f);
        }

        void SetFog(bool underwater)
        {
            RenderSettings.fog = underwater ? true : defaultFog;
            RenderSettings.fogDensity = underwater ? Density : defaultFogDensity;
        }
    }
}