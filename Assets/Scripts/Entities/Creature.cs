using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract class Creature
{
    public int baseSpeed;
    abstract public void Move();
    public Creature(int bSpeed) { baseSpeed = bSpeed; }
}
