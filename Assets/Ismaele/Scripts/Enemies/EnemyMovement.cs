﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : PlayerMovement
{
    // Other properties belonging to the enemy
    private NavMeshAgent enemyAgent;
    public EnemySensor sensor;

    private bool traversingLink;

    protected override void Start()
    {
        base.Start();

        enemyAgent = GetComponent<NavMeshAgent>();

        enemyAgent.updatePosition = false;
        enemyAgent.updateRotation = false;
        enemyAgent.autoTraverseOffMeshLink = false;
    }

    protected override void Update()
    {
        enemyAgent.nextPosition = transform.position;
        PlayerInput input = ResolveInput();

        Move(input);
        RotateModel();
    }

    private PlayerInput ResolveInput()
    {
        PlayerInput enemyInput = new PlayerInput();

        if (!sensor.SensesPlayer)
        {
            enemyInput.moveInput = Vector2.zero;
            enemyInput.jump = true;
            enemyInput.attack = false;
            enemyInput.ability = false;
        } else
        {
            // Moving towards the player
            enemyAgent.SetDestination(sensor.Player.position);

            enemyInput.moveInput = CondenseVector3(enemyAgent.desiredVelocity);

            Debug.Log(enemyAgent.isOnOffMeshLink);

            if (enemyAgent.isOnOffMeshLink && !traversingLink)
            {
                OffMeshLinkData data = enemyAgent.currentOffMeshLinkData;
                enemyInput.moveInput = CondenseVector3(data.endPos - transform.position);

                enemyInput.jump = !IsBelow(data.endPos);

                if (!Controller.isGrounded)
                {
                    traversingLink = true;
                }
            } else if (traversingLink)
            {
                OffMeshLinkData data = enemyAgent.currentOffMeshLinkData;
                enemyInput.moveInput = CondenseVector3(data.endPos - transform.position);
                enemyInput.jump = !IsBelow(data.endPos);

                if (Controller.isGrounded && Vector3.Distance(transform.position, data.endPos) < 2)
                {
                    traversingLink = false;
                    enemyAgent.CompleteOffMeshLink();
                }

            } else
            {
                enemyInput.moveInput = CondenseVector3(enemyAgent.desiredVelocity);

                enemyInput.jump = false;
            }

            enemyInput.attack = false;
            enemyInput.ability = false;
        }

        return enemyInput;
    }

    public bool IsBelow(Vector3 v)
    {
        if(transform.position.y - 2 > v.y)
        {
            return true;
        }

        return false;
    }
}
