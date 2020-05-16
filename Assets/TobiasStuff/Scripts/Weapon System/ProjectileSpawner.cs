using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    public Action OnSpawnTriggered;
    public Action OnSpawn;

    public bool spawnOnStart;
    public float spawnDelay;

    public Projectile projectile;
    public float force;

    private void Start()
    {
        if(spawnOnStart)
        {
            TriggerSpawn();
        }
    }

    public void TriggerSpawn()
    {
        if (spawnDelay > 0)
            Invoke("Fire", spawnDelay);
        else
            Fire();
    }

    public void Fire()
    {
        Projectile instance = Instantiate(projectile, transform.position, transform.rotation);

        if(force > 0)
        {
            instance.SetVelocity(transform.forward * force);
        }
    }
}
