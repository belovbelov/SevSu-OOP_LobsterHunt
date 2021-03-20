using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnAWater : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            Debug.Log("FUCK");

        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
