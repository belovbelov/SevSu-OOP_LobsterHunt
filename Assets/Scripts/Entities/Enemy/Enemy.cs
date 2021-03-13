using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract class Creature
{
    public int baseSpeed;
    abstract public void move();
    public Creature(int bSpeed) { baseSpeed = bSpeed; }
}
class Fish:Creature
{
    public int vision { get; set; }
    public override void move()
    {

    }
    public Fish(int bSpeed, int Vision):base(bSpeed) { vision = Vision; }
}
class EnemyFish:Fish
{
    
    public int attackRange { get; set; }
    public override void move()
    {
        Debug.Log(baseSpeed);
        Debug.Log(vision);
        Debug.Log(attackRange);
        
    }
    public EnemyFish(int baseSpeed,int vision, int ARange):base(baseSpeed,vision) { attackRange = ARange;}
}

public class Enemy : MonoBehaviour
{   
    public GameObject SphereVision;
    
    void Start()
    {
        EnemyFish f1 = new EnemyFish(1, 2, 3);

        f1.move();

    }

    
    // Update is called once per frame
    void Update()
    {
        
    }
}
