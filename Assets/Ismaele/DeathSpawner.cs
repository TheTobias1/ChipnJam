using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthManager))]
public class DeathSpawner : MonoBehaviour
{
    public HealthManager healthSystem;
    public GameObject spawnedObject;

    private void Start()
    {
        healthSystem.onKilled += Spawn;
    }

    private void Update()
    {
        if (healthSystem.IsAlive)
            healthSystem.TakeDamage(1);
    }

    public void Spawn()
    {
        if (spawnedObject != null)
            Instantiate(spawnedObject, Vector3.zero, Quaternion.identity);
    }
}
