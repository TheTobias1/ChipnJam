using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slam : MovementAbility
{
    bool hasDoubleJump = false;
    public ProjectileSpawner Aoe;

    private bool triggeredSlam;
    private float nextBlast;

    public override void ActivateAbility()
    {
        if (hasDoubleJump && nextBlast < Time.time)
        {
            hasDoubleJump = false;
            triggeredSlam = true;
            nextBlast = Time.time + 4f;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (controller.isGrounded)
        {
            hasDoubleJump = true;

            if(triggeredSlam)
            {
                Aoe.TriggerSpawn();
                triggeredSlam = false;
            }
        }
        else if(triggeredSlam)
        {
            Vector3 vel = Vector2.zero;
            vel.y = -1;
            movement.Velocity = vel.normalized * 40;
        }
    }
}
