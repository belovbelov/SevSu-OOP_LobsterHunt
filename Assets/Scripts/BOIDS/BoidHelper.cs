using UnityEngine;

namespace Lobster.BOIDS
{
    public static class BoidHelper
    {
        private const int NumViewDirections = 300;
        public static readonly Vector3[] Directions;

        static BoidHelper()
        {
            Directions = new Vector3[BoidHelper.NumViewDirections];

            float goldenRatio = (1 + Mathf.Sqrt (5)) / 2;
            float angleIncrement = Mathf.PI * 2 * goldenRatio;

            for (int i = 0; i < NumViewDirections; i++)
            {
                float t = (float) i / NumViewDirections;
                float inclination = Mathf.Acos (1 - 2 * t);
                float azimuth = angleIncrement * i;

                float x = Mathf.Sin (inclination) * Mathf.Cos (azimuth);
                float y = Mathf.Sin (inclination) * Mathf.Sin (azimuth);
                float z = Mathf.Cos (inclination);
                Directions[i] = new Vector3(x, y, z);
            }
        }

    }
}