using UnityEngine;

namespace Assets.Scripts.BOIDS
{
    [CreateAssetMenu]
    public class BoidSettings : ScriptableObject {
        // Settings
        public float MinSpeed = 2;
        public float MaxSpeed = 5;
        public float PerceptionRadius = 2.5f;
        public float AvoidanceRadius = 1;
        public float MaxSteerForce = 3;

        public float AlignWeight = 1;
        public float CohesionWeight = 1;
        public float SeperateWeight = 1;

        public float TargetWeight = 1;

        [Header ("Collisions")]
        public LayerMask ObstacleMask;
        public float BoundsRadius = .27f;
        public float AvoidCollisionWeight = 10;
        public float CollisionAvoidDst = 5;

    }
}