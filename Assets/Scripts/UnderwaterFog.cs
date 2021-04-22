using UnityEngine;

namespace Assets.Scripts
{
    public class UnderwaterFog : MonoBehaviour
    {
        public Transform waterPlane;

        private bool defaultFog = false;
        private float defaultFogDensity = 0;

        public float density = 0.075f;

        // Use this for initialization
        void Start()
        {
            defaultFog = RenderSettings.fog;
            defaultFogDensity = RenderSettings.fogDensity;
        }

        // Update is called once per frame
        void Update()
        {
            if (waterPlane == null) return;

            // test the y position to see if it is under the plane
            SetFog(transform.position.y < waterPlane.position.y + 0.425f);

            // test fog according to the bounding box of the water "box")
            // SetFog(waterPlane.gameObject.GetComponent<Renderer>().bounds.Contains(transform.position));

            // using a plane that can be rotated
            SetFog(waterPlane.InverseTransformPoint(transform.position).y < 0.425f);
        }

        void SetFog(bool underwater)
        {
            RenderSettings.fog = underwater ? true : defaultFog;
            RenderSettings.fogDensity = underwater ? density : defaultFogDensity;
        }
    }
}