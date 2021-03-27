using UnityEngine;

public abstract class Creature : MonoBehaviour
{
    public float creatureSpeed = 4.0f;

    abstract public void Move();
}
