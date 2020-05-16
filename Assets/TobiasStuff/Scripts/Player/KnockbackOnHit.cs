using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackOnHit : MonoBehaviour
{
    PlayerMovement movement;
    HealthManager hp;

    public float knockBackForce = 10;

    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<PlayerMovement>();
        hp = GetComponent<HealthManager>();

        hp.onDamaged += OnHit;
    }

    private void OnHit(GameObject enemy)
    {
        Vector3 vel = transform.position - enemy.transform.position;
        vel.y += 0.4f;
        movement.Stun(vel.normalized * knockBackForce);
    }

    private void OnDestroy()
    {
        hp.onDamaged -= OnHit;
    }
}
