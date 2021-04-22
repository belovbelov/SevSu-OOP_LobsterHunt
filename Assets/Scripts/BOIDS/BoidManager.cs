using UnityEngine;

namespace Assets.Scripts.BOIDS
{
    public class BoidManager : MonoBehaviour {
        private const int ThreadGroupSize = 1024;

        public BoidSettings Settings;
        public ComputeShader Compute;
        Boid[] boids;

        private void Start () {
            boids = FindObjectsOfType<Boid> ();
            foreach (Boid b in boids) {
                b.Initialize (Settings, null);
            }

        }

        private void Update () {
            if (boids != null) {

                int numBoids = boids.Length;
                var boidData = new BoidData[numBoids];

                for (int i = 0; i < boids.Length; i++) {
                    boidData[i].Position = boids[i].Position;
                    boidData[i].Direction = boids[i].Forward;
                }

                var boidBuffer = new ComputeBuffer (numBoids, BoidData.Size);
                boidBuffer.SetData (boidData);

                Compute.SetBuffer (0, "boids", boidBuffer);
                Compute.SetInt ("numBoids", boids.Length);
                Compute.SetFloat ("viewRadius", Settings.PerceptionRadius);
                Compute.SetFloat ("avoidRadius", Settings.AvoidanceRadius);

                int threadGroups = Mathf.CeilToInt (numBoids / (float) ThreadGroupSize);
                Compute.Dispatch (0, threadGroups, 1, 1);

                boidBuffer.GetData (boidData);

                for (int i = 0; i < boids.Length; i++) {
                    boids[i].AvgFlockHeading = boidData[i].FlockHeading;
                    boids[i].CentreOfFlockmates = boidData[i].FlockCentre;
                    boids[i].AvgAvoidanceHeading = boidData[i].AvoidanceHeading;
                    boids[i].NumPerceivedFlockmates = boidData[i].NumFlockmates;

                    boids[i].UpdateBoid ();
                }

                boidBuffer.Release ();
            }
        }

        public struct BoidData {
            public Vector3 Position;
            public Vector3 Direction;

            public Vector3 FlockHeading;
            public Vector3 FlockCentre;
            public Vector3 AvoidanceHeading;
            public int NumFlockmates;

            public static int Size {
                get {
                    return sizeof (float) * 3 * 5 + sizeof (int);
                }
            }
        }
    }
}