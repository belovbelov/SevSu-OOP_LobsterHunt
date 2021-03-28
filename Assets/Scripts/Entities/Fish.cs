using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : Creature
{
    protected float fishSpeed;
    protected Fish() : base()
    {
        fishSpeed = creatureSpeed;
    }
    public int vision { get; set; }

    public override void Move()
    {

    }
}
