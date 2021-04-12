using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.ScriptableObjetsGenerator
{
    [CreateAssetMenu(fileName = "New Gun", menuName = "Gun")]

    public class Gun : ScriptableObject
    {
        public string gunName;
        public GameObject prefab;
        public float damage;
        public float range;
        public float aimSpeed;
        public float recoil;
        public float kickback;
        public float firerate;
    }
}
