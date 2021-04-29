using UnityEngine;

namespace Lobster
{
    public class UnderwaterFog : MonoBehaviour
    {
        public Transform WaterPlane;

        private bool defaultFog = true;
        [SerializeField]
        private float defaultFogDensityStart = 50f, defaultFogDensityEnd = 300f;
        [SerializeField]
        private Color32 defaultFogColor = new Color32(255, 255, 255,255);

        public Color32 Color = new Color32(64, 161, 195, 255);
        public float Density = 0.075f;
        // Use this for initialization
        private void Start()
        {
            defaultFog = RenderSettings.fog;
        }

        // Update is called once per frame
        private void Update()
        {
            if (WaterPlane == null) return;
            SetFog(transform.position.y < WaterPlane.position.y + 0.425f);
        }

        void SetFog(bool underwater)
        {
            //RenderSettings.fog = underwater ? true : defaultFog;
            RenderSettings.fogMode = underwater ? FogMode.Exponential : FogMode.Linear;
            if (underwater)
            {
                RenderSettings.fogDensity = Density;
                RenderSettings.fogColor = Color;
            }
            else
            {
                RenderSettings.fogStartDistance = defaultFogDensityStart;
                RenderSettings.fogEndDistance = defaultFogDensityEnd;
                RenderSettings.fogColor = defaultFogColor;
            }
        }
    }
}