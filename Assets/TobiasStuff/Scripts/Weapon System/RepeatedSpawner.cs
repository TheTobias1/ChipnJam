using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatedSpawner : MonoBehaviour
{
    public ProjectileSpawner[] spawners;

    public int numSpawns;
    public float timeBetweenSpawns;

    // Start is called before the first frame update
    void Start()
    {
        Spawn();
    }

    void Spawn()
    {
        Invoke("Spawn", timeBetweenSpawns);
        foreach(ProjectileSpawner s in spawners)
        {
            s.TriggerSpawn();
        }
        numSpawns--;

        if(numSpawns <= 0)
        {
            Destroy(gameObject);
        }
    }
}
