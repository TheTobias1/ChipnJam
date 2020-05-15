﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Rigidbody body;

    //Projectile settings
    public float projectileWidth;
    public float projectileSpeed;
    public float projectileTimeout;

    //Damage
    public int damage;
    public bool damageOnOverlap;
    public float overlapTickRate;
    private float nextOverlap;

    public bool damageOnRaycast;
    public bool destroyOnImpact;

    public LayerMask mask;
    public List<string> damagingTags;

    public GameObject deathPrefab;

    public void Start()
    {
        Invoke("Kill", projectileTimeout);

        if(projectileSpeed > 0)
            SetVelocity(transform.forward * projectileSpeed);
    }

    public void SetVelocity(Vector3 force)
    {
        if(body != null)
            body.velocity = force;
    }

    private void Update()
    {
        if(damageOnRaycast)
        {
            RaycastHit hit = CollisionCheck();
            if(hit.collider != null)
            {
                if(damagingTags.Contains(hit.collider.tag))
                {
                    DamageObject(hit.collider.gameObject);
                }


                //Destroy
                Kill();
            }
        }

        if(damageOnOverlap && Time.time > nextOverlap)
        {
            nextOverlap = Time.time + overlapTickRate;
            Overlap();
        }



    }

    RaycastHit CollisionCheck()
    {
        RaycastHit hit;

        if(projectileWidth > 0.1)
        {
            if(Physics.SphereCast(transform.position, projectileWidth, transform.forward, out hit, projectileSpeed * Time.deltaTime + 1, mask))
            {
                return hit;
            }
        }
        else
        {
            if (Physics.Raycast(transform.position, transform.forward, out hit, projectileSpeed * Time.deltaTime + 1, mask))
            {
                return hit;
            }
        }

        return new RaycastHit();
    }

    void Overlap()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, projectileWidth, mask);

        foreach(Collider h in hits)
        {
            DamageObject(h.gameObject);
        }

    }

    public void DamageObject(GameObject r)
    {
        HealthManager hp = r.GetComponent<HealthManager>();

        if (hp == null)
        {
            hp = r.GetComponentInParent<HealthManager>();
        }

        if (hp != null)
        {
            hp.TakeDamage(damage);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(destroyOnImpact)
        {
            Kill();
        }
    }

    public void Kill()
    {
        CancelInvoke("Kill");

        if(deathPrefab != null)
        {
            Instantiate(deathPrefab, transform.position, transform.rotation);
        }

        Destroy(gameObject);
    }
}
