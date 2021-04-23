using UnityEngine;

namespace Assets.Scripts.Entities
{
    public class Enemy : Fish
    {

        private int current;
        private int colliderCounter;
        public float Dist;
        private float enemySpeed;

        private Enemy()
        {
            enemySpeed = CreatureSpeed;
        }
        private void Start()
        {
            MinSpeed = enemySpeed- MinSpeedBias;
            MaxSpeed = enemySpeed + MaxSpeedBias;
            CachedTransform = transform;
            Position = CachedTransform.position;
            Forward = CachedTransform.forward;
            float startSpeed = (MinSpeed + MaxSpeed) / 2;
            Velocity = transform.forward * startSpeed;
            Target = Points[current];
        }
        private void Update()
        {
            //if (colliderCounter == 0)
            // {
            Move();
            // }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                colliderCounter += 1;
                Target = other.gameObject.transform;
                if (colliderCounter == 2)
                {
                    Player.IsDead = true;

                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                colliderCounter -= 1;
                if (colliderCounter == 0)
                {
                    Target = Points[current];
                }
            }
        }

        public override void Move()
        {
            base.Move();
            transform.rotation *= Quaternion.Euler(0f, 90f, 0f);
            Dist = Vector3.Distance(transform.position, Points[current].position);
            if (Dist < 5)
            {
                current = (current + 1) % Points.Length;
                Target = Points[current];
            }
        }
    }
}