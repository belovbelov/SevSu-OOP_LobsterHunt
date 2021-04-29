using UnityEngine;

namespace Lobster.Entities
{
    public class Enemy : Fish
    {

        private int current;
        private int colliderCounter;
        [SerializeField]
        private float dist;
        private readonly float enemySpeed;

        public Player Player;

        private Enemy()
        {
            enemySpeed = CreatureSpeed;
        }
        private void Start()
        {
            MinSpeed = enemySpeed - MinSpeedBias;
            MaxSpeed = enemySpeed + MaxSpeedBias;
            CachedTransform = transform;
            Position = CachedTransform.position;
            Forward = CachedTransform.forward;
            var startSpeed = (MinSpeed + MaxSpeed) / 2;
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
            dist = Vector3.Distance(transform.position, Points[current].position);
            if (dist < 5)
            {
                current = (current + 1) % Points.Length;
                Target = Points[current];
            }
        }
    }
}