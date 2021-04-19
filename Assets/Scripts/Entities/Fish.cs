using UnityEngine;
using Assets.Scripts;
namespace Assets.Scripts.Entities
{
    public class Fish : Creature
    {
        public float health = 50f;
        public bool inRange = false;
        protected Vector3 position;
        protected Vector3 forward;
        protected Vector3 velocity;
        public Transform target;
        public Transform cachedTransform;

        protected float fishSpeed;

        public float minSpeed = 2;
        public float maxSpeed = 5;
        public float maxSteerForce = 3;
        public float targetWeight = 1;

        public LayerMask obstacleMask;
        public float boundsRadius = .27f;
        public float avoidCollisionWeight = 10;
        public float collisionAvoidDst = 5;

        public Transform[] points;

        protected Fish() : base()
        {
            fishSpeed = creatureSpeed;
        }

        private void Start()
        {
            cachedTransform = transform;
            position = cachedTransform.position;
            forward = cachedTransform.forward;
            float startSpeed = (minSpeed + maxSpeed) / 2;
            velocity = transform.forward * startSpeed;
            target = null;
        }

        private void Update()
        {
            Move();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                inRange = true;
                float PlayerDist,FishDist;
                float maxdist = 0;
                int idist = 0;
                for (int i = 0; i < points.Length; i++)
                {
                    PlayerDist = Vector3.Distance(other.gameObject.transform.position, points[i].position);
                    FishDist = Vector3.Distance(transform.position, points[i].position);
                    if (PlayerDist - FishDist > maxdist)
                    {
                        maxdist = PlayerDist - FishDist;
                        idist = i;
                    }
                }
                target = points[idist];
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                inRange = false;
                target = null;
            }
        }

        public override void Move()
        {
            Vector3 acceleration = Vector3.zero;

            if (target != null)
            {
                Vector3 offsetToTarget = target.position - position;
                acceleration = SteerTowards(offsetToTarget) * targetWeight;
            }
            if (IsHeadingForCollision())
            {
                Vector3 collisionAvoidDir = ObstacleRays();
                Vector3 collisionAvoidForce = SteerTowards(collisionAvoidDir) * avoidCollisionWeight;
                acceleration += collisionAvoidForce;
            }
            velocity += acceleration * Time.deltaTime;
            float speed = velocity.magnitude;
            Vector3 dir = velocity / speed;
            speed = Mathf.Clamp(speed, minSpeed, maxSpeed);
            velocity = dir * speed;

            cachedTransform.position += velocity * Time.deltaTime;
            cachedTransform.forward = dir;
            position = cachedTransform.position;
            forward = dir;
        }

        Vector3 SteerTowards(Vector3 vector)
        {
            Vector3 v = vector.normalized * maxSpeed - velocity;
            return Vector3.ClampMagnitude(v, maxSteerForce);
        }

        bool IsHeadingForCollision()
        {
            RaycastHit hit;
            if (Physics.SphereCast(position, boundsRadius, forward, out hit, collisionAvoidDst, obstacleMask))
            {
                return true;
            }
            else { }
            return false;
        }

        Vector3 ObstacleRays()
        {
            Vector3[] rayDirections = BoidHelper.directions;

            for (int i = 0; i < rayDirections.Length; i++)
            {
                Vector3 dir = cachedTransform.TransformDirection(rayDirections[i]);
                Ray ray = new Ray(position, dir);
                if (!Physics.SphereCast(ray, boundsRadius, collisionAvoidDst, obstacleMask))
                {
                    return dir;
                }
            }

            return forward;
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
            Weapon.fishKilled += 1;
            Destroy(gameObject);
        }

    }
}