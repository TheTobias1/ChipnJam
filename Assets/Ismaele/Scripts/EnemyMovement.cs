using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(SphereCollider))]
public class EnemyMovement : PlayerMovement
{
    // Other properties belonging to the enemy
    private NavMeshAgent enemyAgent;
    private SphereCollider sensingZone;

    // Determine the current state of the enemy
    private bool sensesPlayer;
    private Transform player;

    protected override void Start()
    {
        base.Start();

        enemyAgent = GetComponent<NavMeshAgent>();
        sensingZone = GetComponent<SphereCollider>();

        sensingZone.radius = 4.5f;
        sensingZone.isTrigger = true;
    }

    protected override void Update()
    {
        PlayerInput input = ResolveInput();

        Move(input);
        RotateModel();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            sensesPlayer = true;
            player = other.transform;

            sensingZone.radius = 7.5f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            sensesPlayer = false;
            player = null;

            sensingZone.radius = 4.5f;
        }
    }

    private PlayerInput ResolveInput()
    {
        PlayerInput enemyInput = new PlayerInput();

        if (!sensesPlayer)
        {
            enemyInput.moveInput = Vector2.zero;
            enemyInput.jump = true;
            enemyInput.attack = false;
            enemyInput.ability = false;
        } else
        {
            // Moving towards the player
            Vector2 target = new Vector2(player.position.x, player.position.z);
            Vector2 currentPosition = new Vector2(transform.position.x, transform.position.z);

            enemyInput.moveInput = (target - currentPosition).normalized;

            enemyInput.jump = false;
            enemyInput.attack = false;
            enemyInput.ability = false;
        }

        return enemyInput;
    }
}
