using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class EnemySensor : MonoBehaviour
{
    private SphereCollider sensingZone;

    public float detectRange = 4.5f;
    public float loseSightRange = 7.5f;

    public bool SensesPlayer { get; private set; }
    public Transform Player { get; private set; }

    private void Start()
    {
        sensingZone = GetComponent<SphereCollider>();

        sensingZone.radius = detectRange;
        sensingZone.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            SensesPlayer = true;
            Player = other.transform;

            sensingZone.radius = loseSightRange;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            SensesPlayer = false;
            Player = null;

            sensingZone.radius = detectRange;
        }
    }
}
