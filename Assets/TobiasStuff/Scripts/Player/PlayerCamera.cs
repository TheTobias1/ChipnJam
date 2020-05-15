﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public GameObject player;

    public bool moveToPlayer;
    public float lerpSpeed;
    [Tooltip("Camera will start moving when the target is y distance away and will stop when the target is x distance away")]
    public Vector2 moveThreshold;
    public Vector3 playerOffset;

    private bool centered;
    private Vector3 desired;

    // Start is called before the first frame update
    void Start()
    {
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(moveToPlayer)
        {
            MoveToPlayer();
        }

        transform.position = Vector3.Lerp(transform.position, desired, lerpSpeed * Time.deltaTime);
    }

    void MoveToPlayer()
    {
        Vector3 target = player.transform.position + playerOffset;

        if(!centered)
        {
            MoveCamera(target);

            if (Vector3.Distance(transform.position, target) < moveThreshold.x)
            {
                centered = true;
            }
        }
        else
        {
            if(Vector3.Distance(transform.position, target) > moveThreshold.y)
            {
                centered = false;
            }
        }

    }

    public void MoveCamera(Vector3 pos)
    {
        desired = Vector3.Lerp(transform.position, pos, lerpSpeed * Time.deltaTime);
    }
}
