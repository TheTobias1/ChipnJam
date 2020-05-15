using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Rigidbody body;

    public float projectileWidth;
    public float projectileSpeed;
    public float projectileTimeout;

    public bool damageOnOverlap;
    public bool damageOnCollision;

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
        if(damageOnCollision)
        {
            if(CollisionCheck().collider != null)
            {
                //Do damage


                //Destroy
                Kill();
            }
        }

        if(damageOnOverlap)
        {
            Collider[] hits = Overlap();

            foreach(Collider h in hits)
            {
                //Do damage
            }
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

    Collider[] Overlap()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, projectileWidth, mask);
        List<Collider> goodHits = new List<Collider>();

        foreach(Collider h in hits)
        {
            if(damagingTags.Contains(h.tag))
            {
                goodHits.Add(h);
            }
        }

        return goodHits.ToArray();
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
