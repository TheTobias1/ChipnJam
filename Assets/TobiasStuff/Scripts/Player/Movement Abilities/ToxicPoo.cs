using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToxicPoo : DoubleJump
{
    public ProjectileSpawner Aoe;
    private float nextBlast;

    public override void ActivateAbility()
    {

        if (hasDoubleJump)
        {
            Vector3 vel = movement.Velocity;
            vel.y = movement.jumpForce / 2;
            movement.Velocity = vel;

            if(Time.time > nextBlast)
            {
                Aoe.TriggerSpawn();
                nextBlast = Time.time + 1.75f;
            }

            hasDoubleJump = false;
        }

    }
}