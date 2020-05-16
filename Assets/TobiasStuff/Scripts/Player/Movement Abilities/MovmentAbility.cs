using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovementAbility : MonoBehaviour
{
    protected PlayerMovement movement;
    protected CharacterController controller;

    private void Awake()
    {
        movement = GetComponentInParent<PlayerMovement>();
        controller = GetComponentInParent<CharacterController>();

        if(movement.movementAbility != null)
        {
            Destroy(movement.movementAbility.gameObject);
        }

        movement.movementAbility = this;
    }

    public abstract void ActivateAbility();
}
