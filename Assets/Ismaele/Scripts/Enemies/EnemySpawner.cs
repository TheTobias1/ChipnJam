using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public List<Transform> spawnLocations;
    public float minSpawnTime = 5.0f;
    public float maxSpawnTime = 12.0f;

    private float spawnTime;
    private float timeSinceLast;
    private bool canSpawn;

    private void Start()
    {
        canSpawn = false;
        spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
    }

    private void FixedUpdate()
    {

        if (canSpawn)
        {
            canSpawn = false;
            spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
            int randomLocation = Random.Range(0, spawnLocations.Count);
            Debug.Log(randomLocation);
            Instantiate(enemy, spawnLocations[randomLocation].position, Quaternion.identity);
        } else
        {
            timeSinceLast += Time.deltaTime;
            if (timeSinceLast >= spawnTime)
            {
                timeSinceLast = 0.0f;
                canSpawn = true;
            }
        }
    }
}
