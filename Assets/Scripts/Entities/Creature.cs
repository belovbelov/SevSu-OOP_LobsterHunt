using UnityEngine;

namespace Assets.Scripts.Entities
{
    public abstract class Creature : MonoBehaviour
    {
        [SerializeField]
        protected float CreatureSpeed = 4.0f;

        public abstract void Move();
    }
}