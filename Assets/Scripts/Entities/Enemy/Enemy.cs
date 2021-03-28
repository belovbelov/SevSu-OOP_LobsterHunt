using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Enemy :Fish
{
    Enemy() : base()
    {
        enemySpeed = fishSpeed;
    }
    public Transform[] points;
    int current;
    float enemySpeed;
    int colliderCounter = 0;

    public int attackRange { get; set; }

    Quaternion targetRotation;

    void Start()
    {
        current = 0; 
    }

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
                enemySpeed = 2.0f;
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
                enemySpeed = 4.0f;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            transform.position = Vector3.MoveTowards(transform.position, other.transform.position, enemySpeed * Time.deltaTime * 0.1f);
            transform.rotation = Quaternion.LookRotation(other.transform.position - transform.position) * Quaternion.Euler(0f, 90f, 0f);
        }
    }
}
