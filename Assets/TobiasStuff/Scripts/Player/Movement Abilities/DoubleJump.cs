using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJump : MovementAbility
{
    public bool hasDoubleJump;

    public override void ActivateAbility()
    {
        movement.Velocity.y = movement.jumpForce;
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
