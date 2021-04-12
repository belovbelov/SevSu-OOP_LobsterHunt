using UnityEngine;

namespace Assets.Scripts.Entities
{
    public abstract class Creature : MonoBehaviour
    {
        public float creatureSpeed = 4.0f;

        abstract public void Move();
    }
}