using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sphereRangeTrigger : MonoBehaviour
{
    public AttackRange a;
    public sphereMoveArea m;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("yo");
            m.onTrigger = false;
            a.onTrigger = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            a.onTrigger = false;
            m.onTrigger = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
