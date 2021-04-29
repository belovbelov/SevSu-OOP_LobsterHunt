using UnityEngine;

namespace Lobster.Entities
{
    public abstract class Creature : MonoBehaviour
    {
        [SerializeField]
        protected float CreatureSpeed = 4.0f;

        public abstract void Move();
    }
}