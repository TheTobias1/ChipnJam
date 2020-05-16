using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class EnemyDamage : MonoBehaviour
{
    private BoxCollider hitBox;

    public int damageAmount;

    private void Start()
    {
        hitBox = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            HealthManager playerHealth = other.GetComponent<HealthManager>();
            playerHealth.TakeDamage(damageAmount);
            playerHealth.onDamaged?.Invoke(gameObject);
        }
    }
}
