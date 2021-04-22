using UnityEngine;

namespace Assets.Scripts.BOIDS
{
    public class Boid : MonoBehaviour
    {
        private BoidSettings settings;

        // State
        [HideInInspector]
        public Vector3 Position;
        [HideInInspector]
        public Vector3 Forward;
        Vector3 velocity;

        // To update:
        private Vector3 acceleration;
        [HideInInspector]
        public Vector3 AvgFlockHeading;
        [HideInInspector]
        public Vector3 AvgAvoidanceHeading;
        [HideInInspector]
        public Vector3 CentreOfFlockmates;
        [HideInInspector]
        public int NumPerceivedFlockmates;

        // Cached
        private Material material;
        private Transform cachedTransform;
        private Transform target;

        private void Awake()
        {
            material = transform.GetComponentInChildren<MeshRenderer>().material;
            cachedTransform = transform;
        }

        public void Initialize(BoidSettings settings, Transform target)
        {
            this.target = target;
            this.settings = settings;

            Position = cachedTransform.position;
            Forward = cachedTransform.forward;

            float startSpeed = (settings.MinSpeed + settings.MaxSpeed) / 2;
            velocity = transform.forward * startSpeed;
        }

        public void SetColour(Color col)
        {
            if (material != null)
            {
                material.color = col;
            }
        }

        public void UpdateBoid()
        {
            Vector3 acceleration = Vector3.zero;

            if (target != null)
            {
                Vector3 offsetToTarget = target.position - Position;
                acceleration = SteerTowards(offsetToTarget) * settings.TargetWeight;
            }

            if (NumPerceivedFlockmates != 0)
            {
                CentreOfFlockmates /= NumPerceivedFlockmates;

                Vector3 offsetToFlockmatesCentre = CentreOfFlockmates - Position;

                var alignmentForce = SteerTowards (AvgFlockHeading) * settings.AlignWeight;
                var cohesionForce = SteerTowards (offsetToFlockmatesCentre) * settings.CohesionWeight;
                var seperationForce = SteerTowards (AvgAvoidanceHeading) * settings.SeperateWeight;

                acceleration += alignmentForce;
                acceleration += cohesionForce;
                acceleration += seperationForce;
            }

            if (IsHeadingForCollision())
            {
                Vector3 collisionAvoidDir = ObstacleRays ();
                Vector3 collisionAvoidForce = SteerTowards (collisionAvoidDir) * settings.AvoidCollisionWeight;
                acceleration += collisionAvoidForce;
            }

            velocity += acceleration * Time.deltaTime;
            float speed = velocity.magnitude;
            Vector3 dir = velocity / speed;
            speed = Mathf.Clamp(speed, settings.MinSpeed, settings.MaxSpeed);
            velocity = dir * speed;

            cachedTransform.position += velocity * Time.deltaTime;
            cachedTransform.forward = dir;
            Position = cachedTransform.position;
            Forward = dir;
        }

        bool IsHeadingForCollision()
        {
            RaycastHit hit;
            if (Physics.SphereCast(Position, settings.BoundsRadius, Forward, out hit, settings.CollisionAvoidDst, settings.ObstacleMask))
            {
                return true;
            }
            else { }
            return false;
        }

        Vector3 ObstacleRays()
        {
            Vector3[] rayDirections = BoidHelper.Directions;

            for (int i = 0; i < rayDirections.Length; i++)
            {
                Vector3 dir = cachedTransform.TransformDirection (rayDirections[i]);
                Ray ray = new Ray (Position, dir);
                if (!Physics.SphereCast(ray, settings.BoundsRadius, settings.CollisionAvoidDst, settings.ObstacleMask))
                {
                    return dir;
                }
            }

            return Forward;
        }

        Vector3 SteerTowards(Vector3 vector)
        {
            Vector3 v = vector.normalized * settings.MaxSpeed - velocity;
            return Vector3.ClampMagnitude(v, settings.MaxSteerForce);
        }

    }
}