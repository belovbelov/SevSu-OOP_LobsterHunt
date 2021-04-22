using UnityEngine;
using Assets.Scripts.Entities;

namespace Assets.Scripts.Entities.Enemy
{
    public class Enemy : Fish
    {
        Enemy() : base()
        {

        }

        int current = 0;
        int colliderCounter = 0;
        public float dist;

        private void Start()
        {
            cachedTransform = transform;
            position = cachedTransform.position;
            forward = cachedTransform.forward;
            float startSpeed = (minSpeed + maxSpeed) / 2;
            velocity = transform.forward * startSpeed;
            target = points[current];
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
                target = other.gameObject.transform;
                if (colliderCounter == 2)
                {
                    Player.isDead = true;

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
                    target = points[current];
                }
            }
        }

        public override void Move()
        {
            base.Move();
            dist = Vector3.Distance(transform.position, points[current].position);
            transform.rotation *= Quaternion.Euler(0f, 90f, 0f);
            if (dist < 5)
            {
                current = (current + 1) % points.Length;
                target = points[current];
            }
        }
    }
}