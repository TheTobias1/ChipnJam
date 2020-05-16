using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    private HealthManager hp;
    public GameObject deathPrefab;

    // Start is called before the first frame update
    void Start()
    {
        hp = GetComponent<HealthManager>();
        hp.onKilled += Die;
    }

    public void Die()
    {
        if(deathPrefab != null)
        {
            Instantiate(deathPrefab, transform.position, transform.rotation);
        }

        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        hp.onKilled -= Die;
    }

}
