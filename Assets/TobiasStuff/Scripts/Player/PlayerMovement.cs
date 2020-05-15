﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Components
    public Transform playerModel;

    private CharacterController controller;

    //Movement attributes
    public float speed;
    public float acceleration;
    public float deceleration;
    public float gravity;
    public float jumpForce;

    //Movement State
    private Vector3 velocity;
    private float nextJump;
    private Vector3 forwardDirection;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput inputBuffer = InputManager.GetPlayerInput();

        Move(inputBuffer);
        RotateModel();
    }

    protected virtual void Move(PlayerInput input)
    {
        Vector2 planearVelocity = CondenseVector3(velocity);
        Vector2 moveInput = input.moveInput;

        if(moveInput.magnitude > 0.25f)
        {
            planearVelocity += moveInput * acceleration * Time.deltaTime;
            planearVelocity = Vector2.ClampMagnitude(planearVelocity, speed);
            forwardDirection = new Vector3(planearVelocity.x, 0, planearVelocity.y);
        }
        else
        {
            float curSpeed = planearVelocity.magnitude;
            curSpeed = Mathf.Max(0, curSpeed - deceleration * Time.deltaTime);
            planearVelocity = planearVelocity.normalized * curSpeed;
        }

        velocity = new Vector3(planearVelocity.x, velocity.y, planearVelocity.y);

        if(controller.isGrounded)
        {
            velocity.y = -0.1f;

            if(input.jump && nextJump < Time.time)
            {
                Jump();
            }
        }
        else
        {
            velocity.y -= gravity * Time.deltaTime;

            if (input.jump && nextJump < Time.time)
            {
                //Jump ability
            }
        }

        controller.Move(velocity * Time.deltaTime);
    }

    protected virtual void RotateModel()
    {
        playerModel.transform.rotation = Quaternion.Lerp(playerModel.rotation, Quaternion.LookRotation(forwardDirection), 15 * Time.deltaTime);
    }

    protected virtual void Jump()
    {
        nextJump = Time.time + 0.2f;

        velocity.y = jumpForce;
    }

    //Utility Functions
    public Vector2 CondenseVector3(Vector3 v)
    {
        return new Vector2(v.x, v.z);
    }


}
