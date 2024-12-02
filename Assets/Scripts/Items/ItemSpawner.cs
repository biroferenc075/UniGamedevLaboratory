using System.Collections.Generic;


using UnityEngine;
using Utilities;
using Random = UnityEngine.Random;

namespace Shmup {
    public class ItemSpawner : MonoBehaviour {
        [SerializeField] GameObject itemPrefab;

        [SerializeField] float spawnInterval = 3f;
        [SerializeField] List<Vector3> positions = new();

        private float timer = 0f;
        public void Update()
        {
            timer += Time.deltaTime;
            if(timer > spawnInterval)
            {
                timer = 0;
                Instantiate(itemPrefab, positions[Random.Range(0, positions.Count)], Quaternion.identity, null);
            }
        }
    }
}