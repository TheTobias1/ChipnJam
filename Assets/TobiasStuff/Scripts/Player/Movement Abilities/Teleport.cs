using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : DoubleJump
{
    public float distance = 5;
    public ProjectileSpawner Aoe;

    private float nextBlast;

    public override void ActivateAbility()
    {
        if (hasDoubleJump)
        {
            if (Aoe != null && nextBlast < Time.time)
            {
                Aoe.TriggerSpawn();
                nextBlast = Time.time + 4.5f;
            }

            Vector3 vel = movement.playerModel.transform.forward * distance;
            controller.Move(vel);

            hasDoubleJump = false;
        }

    }
}
