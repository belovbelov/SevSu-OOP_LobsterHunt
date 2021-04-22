using UnityEngine;

namespace Assets.Scripts.Entities
{
    public abstract class Creature : MonoBehaviour
    {
        public float CreatureSpeed = 4.0f;

        public abstract void Move();
    }
}