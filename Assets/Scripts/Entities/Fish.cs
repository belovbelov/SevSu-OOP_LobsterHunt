using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Entities
{
    public class Fish : Creature
    {
        protected float fishSpeed;
        protected Fish() : base() => fishSpeed = creatureSpeed;
        public int Vision { get; set; }

        public override void Move()
        {

        }
    }
}