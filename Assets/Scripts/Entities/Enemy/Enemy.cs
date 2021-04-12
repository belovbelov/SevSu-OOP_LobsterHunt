using Assets.Scripts.Entities;
using UnityEngine;



public class Enemy : Fish
{
    Enemy() : base()
    {
        enemySpeed = fishSpeed;
    }
    public Transform[] points;
    int current = 0;
    float enemySpeed;
    int colliderCounter = 0;

    public int attackRange { get; set; }

    Quaternion targetRotation;

    private void Update()
    {
        if (colliderCounter == 0)
        {
            Move();
        }
    }

    public override void Move()
    {
        if (transform.position != points[current].position)
        {
            targetRotation = Quaternion.LookRotation(points[current].position - transform.position) * Quaternion.Euler(0f, 90f, 0f);
            transform.position = Vector3.MoveTowards(transform.position, points[current].position, enemySpeed * Time.deltaTime);
        }
        else
        {
            current = (current + 1) % points.Length;
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 14f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            colliderCounter += 1;

            if (colliderCounter == 2)
            {
                //logic
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
                //logic
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && colliderCounter == 1)
        {
            transform.position = Vector3.MoveTowards(transform.position, other.transform.position, enemySpeed * Time.deltaTime);
            targetRotation = Quaternion.LookRotation(other.transform.position - transform.position) * Quaternion.Euler(0f, 90f, 0f);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 14f);
        }
    }
}
