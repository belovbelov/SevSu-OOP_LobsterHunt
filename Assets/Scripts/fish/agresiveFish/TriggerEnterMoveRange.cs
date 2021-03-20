using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnterMoveRange : MonoBehaviour
{
    public sphereMoveArea m;
    public AttackRange a;
    public bool goCenter;
    public bool FishOutArea;
    public Transform centerArea;
    // Start is called before the first frame update
    void Start()
    {
        goCenter = false;
    }
    private void OnTriggerEnter(Collider other)
    {     
        if (other.gameObject.name == "Shark" || other.gameObject.name == "Fish1" || other.gameObject.name == "Cube" || other.gameObject.name == "Body" || other.gameObject.name == "Body1")
        {
            Debug.Log("n");
            FishOutArea = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Shark" || other.gameObject.name == "Fish1" || other.gameObject.name == "Cube" || other.gameObject.name == "Body" || other.gameObject.name == "Body")
        {
            FishOutArea = true;
            if (!a.onTrigger)
            {
                Debug.Log("Y");
                m.onTrigger = false;
                goCenter = true;
            }
            
        }
    }
    // Update is called once per frame
    void Update()
    {
        
        if (goCenter)
        {
            m.LoockAtRandomPlace(centerArea.position);
            m.transform.position = Vector3.MoveTowards(m.transform.position, centerArea.position, m.speedMove);
        }
        if (a.onTrigger)
        {
            goCenter = false;
        }

        if (!a.onTrigger && FishOutArea)
        {
            goCenter = true;
            m.onTrigger = false;
            
        }
        if (m.transform.position == centerArea.position)
        {
            m.onTrigger = true;
            goCenter = false;
        }
    }
}
