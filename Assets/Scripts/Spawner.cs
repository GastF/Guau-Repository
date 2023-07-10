using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public List<GameObject> prefabsToSpawn;
    public Transform spawnPoint;       // El punto de generación del objeto
    public float spawnInterval = 1f;    // El intervalo de tiempo entre cada generación
    private float spawnTimer = 0f;      // El temporizador de generación

    private void Update()
    {
        // Incrementa el temporizador
        spawnTimer += Time.deltaTime;

        // Comprueba si ha pasado el tiempo suficiente para generar un nuevo objeto
        if (spawnTimer >= spawnInterval)
        {
            // Genera el objeto y reinicia el temporizador
            SpawnObject();
            spawnTimer = 0f;
        }
    }

    public void SpawnObject()
    {
        // Genera un índice aleatorio dentro del rango de la lista de prefabs
        int randomIndex = Random.Range(0, prefabsToSpawn.Count);

        // Obtiene el prefab aleatorio utilizando el índice
        GameObject randomPrefab = prefabsToSpawn[randomIndex];

        // Crea una instancia del prefab en la posición del spawnPoint y sin rotación
        Instantiate(randomPrefab, spawnPoint.position, Quaternion.identity);
    }
}
