using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class FlashOnHit : MonoBehaviour
{
    private HealthManager hp;
    public MeshRenderer mesh;
    public Material flashMaterial;
    private Material normalMaterial;

    // Start is called before the first frame update
    void Start()
    {
        hp = GetComponent<HealthManager>();

        if(hp == null)
        {
            hp = GetComponentInParent<HealthManager>();
        }

        if(hp != null)
        {
            hp.onDamaged += OnHit;
        }

        normalMaterial = mesh.material;
    }

    public void OnHit()
    {
        CancelInvoke("UnFlash");
        mesh.material = flashMaterial;
        Invoke("UnFlash", 0.1f);
    }

    void UnFlash()
    {
        mesh.material = normalMaterial;
    }

    private void OnDestroy()
    {
        if(hp != null)
        {
            hp.onDamaged -= OnHit;
        }
    }
}
