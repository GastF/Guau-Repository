using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TubeSpawner : MonoBehaviour
{
    [System.Serializable]
    public class SpawnObject
    {
        public GameObject prefab;
        public float spawnInterval;
        public Transform spawnPoint;
    }

    public List<SpawnObject> objectsToSpawn;
 

    private float[] spawnTimers;

    private void Start()
    {
        spawnTimers = new float[objectsToSpawn.Count];
    }

    private void Update()
    {
        for (int i = 0; i < objectsToSpawn.Count; i++)
        {
            spawnTimers[i] += Time.deltaTime;

            if (spawnTimers[i] >= objectsToSpawn[i].spawnInterval)
            {
                SpawnObjects(i);
                spawnTimers[i] = 0f;
            }
        }
    }

    private void SpawnObjects(int index)
    {
        Transform spawnPoint = objectsToSpawn[index].spawnPoint;
        GameObject prefab = objectsToSpawn[index].prefab;
        GameObject spawnedObject = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
    }
}
