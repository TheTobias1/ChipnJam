using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LpPickup : MonoBehaviour
{
    public Abilities ability;
    public GameObject lpPickupObject;

    private void Update()
    {
        if(EnemySpawner.islandComplete && !lpPickupObject.activeSelf)
        {
            lpPickupObject.SetActive(true);
        }

        transform.Rotate(new Vector3(0, 0.1f, 0));
    }

    private void OnTriggerEnter(Collider other)
    {
        if(lpPickupObject.activeSelf)
        {
            //pickup
            LpPlayer.acquiredLPs.Add(ability);
            Destroy(gameObject);
        }
    }
}
