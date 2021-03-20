using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackRange : MonoBehaviour
{
    public GameObject player;
    public GameObject fish;
    public Transform moveArea;
    public float dist;
    public float attackRange = 15;
    public GameObject range;
    public Vector3 direction;
    public bool onTrigger;
    public float damping = 14f;
    public float speed;

    
    // Start is called before the first frame update
    void Start()
    {
        fish.transform.position = moveArea.position;

    }
    

    public void LoockAtPlayer()
    {
        Quaternion rotation = Quaternion.LookRotation(player.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
    }
    
    private void FixedUpdate()
    {
        
        direction = player.transform.position;
        dist = Vector3.Distance(player.transform.position, transform.position);
         
        
    }

    // Update is called once per frame
    void Update()
    {
        if (onTrigger )
        {
            LoockAtPlayer();
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed);
        }
        


    }
}
