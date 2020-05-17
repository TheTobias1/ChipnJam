using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPortal : MonoBehaviour
{
    public float delay;
    public GameObject enemyPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        Invoke("SpawnEnemy", delay);
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        Invoke("Kill", 3);
    }

    void Kill()
    {
        Destroy(gameObject);
    }
}
