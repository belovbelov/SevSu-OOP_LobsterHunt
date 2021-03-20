using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sphereMoveArea : MonoBehaviour
{
     
    public float speedMove = 0.0001f; // Скорость движения во время стрейфа
    const float strafeTime = 5.0f; // Сколько секунд будет длиться стрейф
    const float repeatTime = 6.0f;// Через сколько секунд будет повтор стрейфа
    public float damping = 14f;

    public static float directionX = 0;
    public static float directionY = 0;
    public static float directionZ = 0;
    
    public bool nextMove = true;
    public bool onTrigger;
    void Start()
    {   // Время стрейфа не может быть больше
        // чем время повтора, иначе будет сбой
        if (repeatTime > strafeTime)
        {
            InvokeRepeating("Strafe", 0.0f, repeatTime);
        }
        directionX = 0;
        directionY = 0;
        directionZ = 0;

         
    }
    public Vector3 v = new Vector3(directionX, directionY, directionZ);
    public Vector3 lastPos;
    public void LoockAtRandomPlace(Vector3 v)
    {
        Quaternion rotation = Quaternion.LookRotation(v - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
    }
    void Strafe()
    {
        StartCoroutine(c_Strafe());
    }

    IEnumerator c_Strafe()
    {
        float elapsedTime = 0.0f;

       
            directionX = Random.Range(-1000, 1000);
            directionY = Random.Range(-1000, 1000);
            directionZ = Random.Range(-1000, 1000);

        lastPos = v;
        v = new Vector3(directionX, directionY, directionZ);
        


        while (true)
        {
            if (onTrigger)
            {
                LoockAtRandomPlace(v);
                transform.position = Vector3.MoveTowards(transform.position, v, speedMove); 
            }
           
           
            //transform.position += Vector3.right * direction *  * Time.deltaTime;

            elapsedTime += Time.deltaTime;

            if (elapsedTime > strafeTime)
            {
                yield break;
            }

            yield return null;
        }
    }
}
// Update is called once per frame

