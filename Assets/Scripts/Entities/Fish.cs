using Assets.Scripts.BOIDS;
using UnityEngine;

namespace Assets.Scripts.Entities
{
    public class Fish : Creature
    {
        private float health = 50f;
        public bool InRange;
        protected Vector3 Position;
        protected Vector3 Forward;
        protected Vector3 Velocity;
        [SerializeField]
        protected Transform Target;
        [SerializeField]
        protected Transform CachedTransform;

        protected float FishSpeed;
        [SerializeField]
        protected float MinSpeedBias = 2;
        [SerializeField]
        protected float MaxSpeedBias = 5;
        [SerializeField]
        protected float MaxSteerForce = 3;
        [SerializeField]
        protected float TargetWeight = 1;

        public LayerMask ObstacleMask;
        public float BoundsRadius = .27f;
        public float AvoidCollisionWeight = 10;
        public float CollisionAvoidDst = 5;

        protected float MaxSpeed;
        protected float MinSpeed;
        protected Transform[] Points;

        protected Fish()
        {
            FishSpeed = CreatureSpeed;
        }

        private void Start()
        {
            MinSpeed = FishSpeed - MinSpeedBias;
            MaxSpeed = FishSpeed + MaxSpeedBias;
            CachedTransform = transform;
            Position = CachedTransform.position;
            Forward = CachedTransform.forward;
            float startSpeed = (MinSpeed + MaxSpeed) / 2;
            Velocity = transform.forward * startSpeed;
            Target = null;
        }

        private void Update()
        {
            Move();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                InRange = true;
                float PlayerDist,FishDist;
                float maxdist = 0;
                int idist = 0;
                for (int i = 0; i < Points.Length; i++)
                {
                    PlayerDist = Vector3.Distance(other.gameObject.transform.position, Points[i].position);
                    FishDist = Vector3.Distance(transform.position, Points[i].position);
                    if (PlayerDist - FishDist > maxdist)
                    {
                        maxdist = PlayerDist - FishDist;
                        idist = i;
                    }
                }
                Target = Points[idist];
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                InRange = false;
                Target = null;
            }
        }

        public override void Move()
        {
            Vector3 acceleration = Vector3.zero;

            if (Target != null)
            {
                Vector3 offsetToTarget = Target.position - Position;
                acceleration = SteerTowards(offsetToTarget) * TargetWeight;
            }
            if (IsHeadingForCollision())
            {
                Vector3 collisionAvoidDir = ObstacleRays();
                Vector3 collisionAvoidForce = SteerTowards(collisionAvoidDir) * AvoidCollisionWeight;
                acceleration += collisionAvoidForce;
            }
            Velocity += acceleration * Time.deltaTime;
            float speed = Velocity.magnitude;
            Vector3 dir = Velocity / speed;
            speed = Mathf.Clamp(speed, MinSpeed, MaxSpeed);
            Velocity = dir * speed;

            CachedTransform.position += Velocity * Time.deltaTime;
            CachedTransform.forward = dir;
            Position = CachedTransform.position;
            Forward = dir;
        }

        Vector3 SteerTowards(Vector3 vector)
        {
            Vector3 v = vector.normalized * MaxSpeed - Velocity;
            return Vector3.ClampMagnitude(v, MaxSteerForce);
        }

        bool IsHeadingForCollision()
        {
            RaycastHit hit;
            if (Physics.SphereCast(Position, BoundsRadius, Forward, out hit, CollisionAvoidDst, ObstacleMask))
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
                Vector3 dir = CachedTransform.TransformDirection(rayDirections[i]);
                Ray ray = new Ray(Position, dir);
                if (!Physics.SphereCast(ray, BoundsRadius, CollisionAvoidDst, ObstacleMask))
                {
                    return dir;
                }
            }

            return Forward;
        }
        public void TakeDamage(float amountDamage)
        {
            health -= amountDamage;
            if (health <= 0f)
            {
                Die();
            }
        }
        void Die()
        {
            //Дописать скрипт начисления очков+смерти объекта
            Weapon.FishKilled += 1;
            Destroy(gameObject);
        }

    }
}