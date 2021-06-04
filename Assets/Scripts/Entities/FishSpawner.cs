using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Lobster.Entities
{
    public class FishSpawner : MonoBehaviour
    {
        public int SpawnCount = 3;
        public float SpawnRadius = 10;
        public Fish prefab;

        private void Awake()
        {
                Spawn();
        }

        private void Update()
        {
            var fishCount = GameObject.FindGameObjectsWithTag("Fish");
            if (fishCount.Length <= 1)
            {
                Spawn();
            }
        }

        private void Spawn()
        {
            for (int i = 0; i < SpawnCount; i++)
            {
                Vector3 pos = transform.position + Random.insideUnitSphere * SpawnRadius;
                Fish fish = Instantiate (prefab);
                fish.transform.position = pos;
                fish.transform.forward = Random.insideUnitSphere;
            }
        }
    }
}