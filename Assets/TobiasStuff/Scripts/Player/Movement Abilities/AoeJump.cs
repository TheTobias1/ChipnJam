using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoeJump : MovementAbility
{
    public ProjectileSpawner Aoe;

    private float nextBlast;

    private void Start()
    {
        movement.OnJump += Blast;
    }

    public void Blast()
    {
        if(Time.time > nextBlast)
        {
            Aoe.TriggerSpawn();
            nextBlast = Time.time + 2f;
        }
    }

    public override void ActivateAbility()
    {
        return;
    }

    private void OnDestroy()
    {
        movement.OnJump -= Blast;
    }
}
