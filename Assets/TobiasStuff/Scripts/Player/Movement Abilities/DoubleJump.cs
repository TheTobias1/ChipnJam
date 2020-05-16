using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJump : MovementAbility
{
    public bool hasDoubleJump;

    public override void ActivateAbility()
    {
        if(hasDoubleJump)
        {
            Vector3 vel = movement.Velocity;
            vel.y = movement.jumpForce;
            movement.Velocity = vel;

            hasDoubleJump = false;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(controller.isGrounded)
        {
            hasDoubleJump = true;
        }
    }
}
