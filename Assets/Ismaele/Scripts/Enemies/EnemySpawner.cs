using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static bool islandComplete;

    private GameObject player;

    public GameObject enemy;
    public List<Transform> spawnLocations;
    public float minSpawnTime = 5.0f;
    public float maxSpawnTime = 12.0f;

    private float spawnTime;
    private float timeSinceLast;
    private bool canSpawn;

    public int numEnemies;
    private List<GameObject> spawnedEnemies = new List<GameObject>();

    bool waveSpawned = false;
    bool allDead = false;
    bool checkingDead = false;

    private void Awake()
    {
        EnemySpawner.islandComplete = false;
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        canSpawn = false;
        spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
    }

    private void FixedUpdate()
    {
        if (canSpawn && !waveSpawned)
        {
            canSpawn = false;
            spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
            int randomLocation = Random.Range(0, spawnLocations.Count);
            Debug.Log(randomLocation);
            if(enemy != null)
            {
                GameObject e = Instantiate(enemy, spawnLocations[randomLocation].position, Quaternion.identity);
                spawnedEnemies.Add(e);
            }


            --numEnemies;
        } else
        {
            timeSinceLast += Time.deltaTime;
            if (timeSinceLast >= spawnTime)
            {
                timeSinceLast = 0.0f;
                canSpawn = true;
            }
        }

        if (numEnemies <= 0)
            waveSpawned = true;


        if(!checkingDead && waveSpawned)
        {
            checkingDead = true;
            StartCoroutine(DeathCheck());
        }

        if(allDead)
        {
            EnemySpawner.islandComplete = true;
            Debug.Log("Island complete");
            StopAllCoroutines();
            Destroy(gameObject);
        }
    }

    IEnumerator DeathCheck()
    {
        bool d = true;
        foreach(GameObject e in spawnedEnemies)
        {
            yield return null;
            if(e != null)
            {
                d = false;
                break;
            }
        }

        allDead = d;
        checkingDead = false;
    }
}
